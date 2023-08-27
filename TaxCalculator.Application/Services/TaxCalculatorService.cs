using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaxCalculator.Application.Models.Dtos;
using TaxCalculator.Application.Models.Requests;
using TaxCalculator.Application.Models.Response;
using TaxCalculator.Application.Utility;
using TaxCalculator.Domain.Core.Entities;
using TaxCalculator.Domain.Core.Enums;
using TaxCalculator.Domain.Core.Repositories;
using TaxCalculator.Domain.Core.Shared;

namespace TaxCalculator.Application.Services
{
    public class TaxCalculatorService : ITaxCalculatorService
    {
        #region Fields

        private readonly ITaxRepository _taxRepository;

        #endregion

        #region Constructors

        public TaxCalculatorService(ITaxRepository taxRepository)
        { 
            _taxRepository = taxRepository ?? throw new ArgumentNullException(nameof(taxRepository));
        }

        #endregion

        #region Public Methods

        public async Task<Result<TaxCalculationDto>> CalculateTax(CalculateTaxRequest request, CancellationToken cancellationToken)
        {
            Result<TaxCalculationDto> result = Result.Failure<TaxCalculationDto>("Initilization");

            var taxType = await _taxRepository.GetTaxType(request.PostalCode, cancellationToken);
            if (taxType == null) 
            {
                return Result.Failure<TaxCalculationDto>("Tax type not found.");
            }

            result = taxType.Type switch 
            { 
                TaxTypeEnum.FlatValue => await CalculateFlatValueTax(request.AnnualIncome, cancellationToken),
                TaxTypeEnum.FlatRate => await CalculateFlatRateTax(request.AnnualIncome, cancellationToken),
                TaxTypeEnum.Progressive => await CalculateProgressiveTax(request.AnnualIncome, cancellationToken),
                _ => Result.Failure<TaxCalculationDto>($"{taxType.Type} calculation is not implemented.")
            };

            if (result.IsSuccessful)
            {
                var taxData = result.Data;
                await _taxRepository.PersistTaxCalculation(CalculatedTax.Create(taxData.AnnualIncome, taxData.TaxAmount, request.PostalCode), cancellationToken);
            }            

            return result;
        }

        public async Task<Result<TaxInformationDto>> GetTaxInformation(CancellationToken cancellationToken)
        {
            Result<TaxInformationDto> result = Result.Failure<TaxInformationDto>("Initilization");

            var flatTaxResult = await _taxRepository.GetFlatValueTax(cancellationToken);
            var flatRateResult = await _taxRepository.GetFlatRateTax(cancellationToken);
            var progressiveResult = await _taxRepository.GetAllTaxRates(cancellationToken);

            var taxInformation = new TaxInformationDto
            {
                FlatValueTax = new FlatValueTaxDto
                {
                    TaxType = TaxTypeEnum.FlatValue,
                    TaxAmount = flatTaxResult?.FlatValue,
                    Threshold = flatTaxResult?.Threshold,
                    ThresholdRate = flatTaxResult?.ThresholdRate,
                },
                FlatRateTax = new FlatRateTaxDto
                {
                    TaxType = TaxTypeEnum.FlatRate,
                    Rate = flatRateResult?.FlatRate
                },
                ProgressiveTax = from tax in progressiveResult
                                 select new ProgressiveTaxDto
                                 {
                                     TaxType = TaxTypeEnum.Progressive,
                                     Rate = tax?.Rate,
                                     Level = tax?.TaxLevel,
                                     MinValue = tax?.MinValue,
                                     MaxValue = tax?.MaxValue
                                 }
            };

            return Result.Success(taxInformation);
        }

        public async Task<Result<IEnumerable<CalculatedTaxDto>>> GetCalculatedTax(CancellationToken cancellationToken)
        {
            var calculatedTax = _taxRepository.GetAll<CalculatedTax>();
            if (calculatedTax == null)
            {
                return Result.Failure<IEnumerable<CalculatedTaxDto>>("");
            }

            var calcResults = from tax in calculatedTax
                            select new CalculatedTaxDto 
                            {
                                Id = tax.Id,
                                AnnualIncome = tax.AnnualIncome,
                                TaxAmount = tax.TaxAmount,
                                PostalCode = tax.PostalCode,
                                Created = tax.Created
                            };

            return Result.Success(calcResults);
        }

        #endregion

        #region Private Methods

        private async Task<Result<TaxCalculationDto>> CalculateFlatValueTax(decimal annualIncome, CancellationToken cancellationToken)
        {
            var result = Result.Failure<TaxCalculationDto>("Initilization");
            decimal taxAmount;

            var flatValueTax = await _taxRepository.GetFlatValueTax(cancellationToken);
            if (flatValueTax == null) 
            {
                result = Result.Failure<TaxCalculationDto>("Flat Value tax rate not found.");
                return result;
            }

            if (annualIncome < flatValueTax.Threshold)
            {
                taxAmount = annualIncome * PercentageUtil.GetPercentage(flatValueTax.ThresholdRate);
            }
            else
            {
                taxAmount = flatValueTax.FlatValue;
            }

            var taxCalculation = new TaxCalculationDto 
            { 
                AnnualIncome = annualIncome,
                IncomeAfterTax = annualIncome - taxAmount,
                TaxAmount = taxAmount,
                TaxMethod = new TaxMethodDto 
                { 
                    TaxType = TaxTypeEnum.FlatValue,
                    TaxDescription = $"{flatValueTax.FlatValue} per year, else if the individual earns less that {flatValueTax.Threshold} per year the tax will be at {flatValueTax.ThresholdRate}%"
                }
            };

            result = Result.Success(taxCalculation);

            return result;
        }

        private async Task<Result<TaxCalculationDto>> CalculateFlatRateTax(decimal annualIncome, CancellationToken cancellationToken)
        {
            var result = Result.Failure<TaxCalculationDto>("Initilization");

            var flatRateTax = await _taxRepository.GetFlatRateTax(cancellationToken);
            if (flatRateTax == null)
            {
                result = Result.Failure<TaxCalculationDto>("Flat Rate tax rate not found.");
                return result;
            }

            decimal taxAmount = annualIncome * PercentageUtil.GetPercentage(flatRateTax.FlatRate);

            var taxCalculation = new TaxCalculationDto
            {
                AnnualIncome = annualIncome,
                IncomeAfterTax = annualIncome - taxAmount,
                TaxAmount = taxAmount,
                TaxMethod = new TaxMethodDto
                {
                    TaxType = TaxTypeEnum.FlatRate,
                    TaxDescription = $"All users pay {flatRateTax.FlatRate}% tax on their income."
                }
            };

            result = Result.Success(taxCalculation);

            return result;
        }

        private async Task<Result<TaxCalculationDto>> CalculateProgressiveTax(decimal annualIncome, CancellationToken cancellationToken)
        {
            decimal taxAmount;

            var taxRates = await _taxRepository.GetAllTaxRates(cancellationToken);
            if (taxRates == null || !taxRates.Any())
            {
                return Result.Failure<TaxCalculationDto>("Progressive tax rate not found.");
            }

            
            var response = PerformProgressiveTaxCalculation(annualIncome, taxRates);
            if (!response.IsSuccessful)
            {
                return Result.Failure<TaxCalculationDto>(response.ErrorMessage);
            }

            var taxCalculation = new TaxCalculationDto
            {
                AnnualIncome = annualIncome,
                IncomeAfterTax = annualIncome - response.Data.TaxAmount,
                TaxAmount = response.Data.TaxAmount,
                TaxMethod = new TaxMethodDto
                {
                    TaxType = TaxTypeEnum.Progressive,
                    TaxDescription = $""
                }
            };

            return Result.Success(taxCalculation);
        }

        private Result<ProgressiveTaxResponse> PerformProgressiveTaxCalculation(decimal annualIncome, List<TaxRate> taxRates)
        {
            var response = new ProgressiveTaxResponse();
            //var taxRate = taxRates.FirstOrDefault(rate => annualIncome >= rate.MinValue && annualIncome <= rate.MaxValue);
            decimal totalTaxAmount = 0;
            taxRates.ToList().ForEach(rate =>
            {
                decimal taxAmount = Math.Min(annualIncome, rate.MaxValue) - (totalTaxAmount * (1 - PercentageUtil.GetPercentage(rate.Rate)));
                if (taxAmount <= 0)
                {
                    return;
                }

                decimal levelTaxAmount = taxAmount * PercentageUtil.GetPercentage(rate.Rate);
                totalTaxAmount += levelTaxAmount;
            });

            return Result.Success(response);
        }

        #endregion
    }
}

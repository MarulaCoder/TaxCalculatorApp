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

            var flatValueTaxResult = await _taxRepository.GetFlatValueTax(cancellationToken);
            var flatRateTaxResult = await _taxRepository.GetFlatRateTax(cancellationToken);
            var progressiveTaxResult = await _taxRepository.GetProgressiveTaxRates(cancellationToken);

            var taxInformation = new TaxInformationDto
            {
                FlatValueTax = new FlatValueTaxDto
                {
                    Id = flatValueTaxResult.Id,
                    TaxType = TaxTypeEnum.FlatValue,
                    TaxAmount = flatValueTaxResult?.FlatValue,
                    Threshold = flatValueTaxResult?.Threshold,
                    ThresholdRate = flatValueTaxResult?.ThresholdRate,
                },
                FlatRateTax = new FlatRateTaxDto
                {
                    Id = flatRateTaxResult.Id,
                    TaxType = TaxTypeEnum.FlatRate,
                    Rate = flatRateTaxResult?.FlatRate
                },
                ProgressiveTax = from progressiveTax in progressiveTaxResult
                                 select new ProgressiveTaxDto
                                 {
                                     Id = progressiveTax.Id,
                                     TaxType = TaxTypeEnum.Progressive,
                                     Rate = progressiveTax?.Rate,
                                     MinValue = progressiveTax?.MinValue,
                                     MaxValue = progressiveTax?.MaxValue,
                                     AdditionalInformation = progressiveTax?.AdditionalInformation
                                 }
            };

            return Result.Success(taxInformation);
        }

        public async Task<Result<IEnumerable<CalculatedTaxDto>>> GetCalculatedTax(CancellationToken cancellationToken)
        {
            var calculatedTax = _taxRepository.GetAll<CalculatedTax>();
            if (calculatedTax == null || !calculatedTax.Any())
            {
                return Result.Failure<IEnumerable<CalculatedTaxDto>>("No calculated tax found.");
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

        public async Task<Result<IEnumerable<string>>> GetPostalCodes(CancellationToken cancellationToken)
        {
            var taxTypes = _taxRepository.GetAll<TaxType>();
            if (taxTypes == null || !taxTypes.Any())
            {
                return Result.Failure<IEnumerable<string>>("");
            }

            var postalCodes = from types in taxTypes
                              select types.Code;

            return Result.Success(postalCodes);
        }

        public async Task<Result> UpdateProgressiveTax(UpdateProgressiveTaxDto model, CancellationToken cancellationToken)
        {
            var progressiveTax = await _taxRepository.GetByIdAsync<ProgressiveTax>(model.Id, cancellationToken);
            if (progressiveTax == null) 
            {
                return Result.Failure($"Progressive tax with {model.Id} not found.");
            }

            progressiveTax.Update(model.Rate, model.MinValue, model.MaxValue);
            await _taxRepository.UpdateAsync(progressiveTax, cancellationToken);
            return Result.Success(progressiveTax);
        }

        public async Task DeleteProgressiveTax(int id, CancellationToken cancellationToken)
        {
            await _taxRepository.DeleteAsync<ProgressiveTax>(id, cancellationToken);
        }

        public async Task DeleteCalculatedTax(int id, CancellationToken cancellationToken)
        {
            await _taxRepository.DeleteAsync<CalculatedTax>(id, cancellationToken);
        }

        #endregion

        #region Private Methods

        private async Task<Result<TaxCalculationDto>> CalculateFlatValueTax(decimal annualIncome, CancellationToken cancellationToken)
        {
            decimal taxAmount;

            var flatValueTax = await _taxRepository.GetFlatValueTax(cancellationToken);
            if (flatValueTax == null) 
            {
                return Result.Failure<TaxCalculationDto>("Flat Value tax rate not found.");
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
                    TaxDescription = $"{string.Format("{0:C}", flatValueTax.FlatValue)} per year, else if the individual earns less that {string.Format("{0:C}", flatValueTax.Threshold)} per year the tax will be at {string.Format("{0:0.0}", flatValueTax.ThresholdRate)}%"
                }
            };

            return Result.Success(taxCalculation);
        }

        private async Task<Result<TaxCalculationDto>> CalculateFlatRateTax(decimal annualIncome, CancellationToken cancellationToken)
        {
            var flatRateTax = await _taxRepository.GetFlatRateTax(cancellationToken);
            if (flatRateTax == null)
            {
                return Result.Failure<TaxCalculationDto>("Flat Rate tax rate not found.");
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
                    TaxDescription = $"All users pay {string.Format("{0:0.0}", flatRateTax.FlatRate)}% tax on their income."
                }
            };

            return Result.Success(taxCalculation);
        }

        private async Task<Result<TaxCalculationDto>> CalculateProgressiveTax(decimal annualIncome, CancellationToken cancellationToken)
        {
            decimal taxAmount;

            var taxRates = await _taxRepository.GetProgressiveTaxRates(cancellationToken);
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
                    TaxDescription = response.Data.TaxDescription
                }
            };

            return Result.Success(taxCalculation);
        }

        private Result<ProgressiveTaxResponse> PerformProgressiveTaxCalculation(decimal annualIncome, List<ProgressiveTax> taxRates)
        {                       
            decimal totalTaxAmount = 0;
            ProgressiveTax currentTaxRate = null;
            foreach(var rate in taxRates)
            {
                decimal taxAmount = Math.Min(annualIncome, rate.MaxValue);
                if (taxAmount <= 0)
                {
                    break;
                }

                currentTaxRate = rate;
                decimal levelTaxAmount = taxAmount * PercentageUtil.GetPercentage(rate.Rate);
                totalTaxAmount += levelTaxAmount;

                annualIncome -= taxAmount;
            };

            var response = new ProgressiveTaxResponse
            {
                TaxAmount = totalTaxAmount,
                TaxDescription = currentTaxRate != null ? $"A {currentTaxRate?.Rate}% was applied to the annual income. See tax information for more info." : string.Empty
            };

            return Result.Success(response);
        }

        #endregion
    }
}

﻿using TaxCalculator.Application.Models.Dtos;
using TaxCalculator.Application.Models.Requests;
using TaxCalculator.Domain.Core.Shared;

namespace TaxCalculator.Application.Services
{
    public interface ITaxCalculatorService
    {
        Task<Result<TaxCalculationDto>> CalculateTax(CalculateTaxRequest request, CancellationToken cancellationToken);

        Task<Result<TaxInformationDto>> GetTaxInformation(CancellationToken cancellationToken);

        Task<Result<IEnumerable<CalculatedTaxDto>>> GetCalculatedTax(CancellationToken cancellationToken);

        Task<Result<IEnumerable<string>>> GetPostalCodes(CancellationToken cancellationToken);

        Task<Result> UpdateProgressiveTax(UpdateProgressiveTaxDto model, CancellationToken cancellationToken);

        Task DeleteProgressiveTax(int id, CancellationToken cancellationToken);

        Task DeleteCalculatedTax(int id, CancellationToken cancellationToken);
    }
}

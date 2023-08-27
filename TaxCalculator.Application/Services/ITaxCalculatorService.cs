using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Application.Models.Dtos;
using TaxCalculator.Application.Models.Requests;
using TaxCalculator.Domain.Core.Shared;

namespace TaxCalculator.Application.Services
{
    public interface ITaxCalculatorService
    {
        Task<Result<TaxCalculationDto>> CalculateTax(CalculateTaxRequest request, CancellationToken cancellationToken);

        Task<Result<TaxInformationDto>> GetTaxInformation(CancellationToken cancellationToken);

        Task<Result<IEnumerable<CalculatedTaxDto>>> GetCalculatedTax(CancellationToken cancellationToken);
    }
}

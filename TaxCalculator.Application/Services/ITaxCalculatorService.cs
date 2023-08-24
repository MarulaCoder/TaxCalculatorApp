using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Application.Models.Requests;
using TaxCalculator.Domain.Shared;

namespace TaxCalculator.Application.Services
{
    public interface ITaxCalculatorService
    {
        Task<Result> CalculateTax(CalculateTaxRequest request, CancellationToken cancellationToken);
    }
}

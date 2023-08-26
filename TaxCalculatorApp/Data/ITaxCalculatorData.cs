using TaxCalculatorApp.Models.Core;
using TaxCalculatorApp.Models.Requests;

namespace TaxCalculatorApp.Data
{
    public interface ITaxCalculatorData
    {
        Task<TaxCalculation> CalculateTax(TaxCalculateRequest calculateRequest, CancellationToken cancellationToken);

        Task<TaxInformation> GetTaxInformation(CancellationToken cancellationToken);
    }
}

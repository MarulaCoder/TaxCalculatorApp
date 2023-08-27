using TaxCalculatorAppUI.Models.Core;
using TaxCalculatorAppUI.Models.Requests;

namespace TaxCalculatorAppUI.Services
{
    public interface ITaxService
    {
        Task<TaxCalculation> CalculateTax(TaxCalculateRequest calculateRequest, CancellationToken cancellationToken);

        Task<TaxInformation> GetTaxInformation(CancellationToken cancellationToken);
    }
}

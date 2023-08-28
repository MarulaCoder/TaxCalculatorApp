using TaxCalculatorAppUI.Models.Core;
using TaxCalculatorAppUI.Models.Requests;

namespace TaxCalculatorAppUI.Services
{
    public interface ITaxService
    {
        Task<TaxCalculation> CalculateTax(TaxCalculateRequest calculateRequest);

        Task<TaxInformation> GetTaxInformation();

        Task<IEnumerable<TaxHistory>> GetCalculatedTaxHistory();

        Task<IEnumerable<string>> GetPostalCodes();

        Task DeleteTaxHistoryItem(int id);

        Task UpdateProgressiveTaxItem(ProgressiveTax progressiveTax);

        Task DeleteProgressiveTaxItem(int id);
    }
}

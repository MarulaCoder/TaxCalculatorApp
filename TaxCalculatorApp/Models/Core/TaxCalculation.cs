using TaxCalculator.Application.Models.Dtos;

namespace TaxCalculatorApp.Models.Core
{
    public class TaxCalculation
    {
        #region Properties

        public decimal AnnualIncome { get; set; }
        public TaxMethod TaxMethod { get; set; }
        public decimal IncomeAfterTax { get; set; }
        public decimal TaxAmount { get; set; }

        #endregion
    }
}

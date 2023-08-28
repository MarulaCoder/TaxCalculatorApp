namespace TaxCalculator.Application.Models.Dtos
{
    public class TaxCalculationDto
    {
        #region Properties

        public decimal AnnualIncome { get; set; }
        public TaxMethodDto TaxMethod { get; set; }
        public decimal IncomeAfterTax { get; set; }
        public decimal TaxAmount { get; set; }

        #endregion
    }
}

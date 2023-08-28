namespace TaxCalculator.Application.Models.Requests
{
    public class CalculateTaxRequest
    {
        #region Properties

        public decimal AnnualIncome { get; set; }
        public string PostalCode { get; set; }

        #endregion
    }
}

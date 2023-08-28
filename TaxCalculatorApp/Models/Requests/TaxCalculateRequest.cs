namespace TaxCalculatorApp.Models.Requests
{
    public class TaxCalculateRequest
    {
        #region Properties

        public decimal AnnualIncome { get; set; }
        public string PostalCode { get; set; }

        #endregion
    }
}

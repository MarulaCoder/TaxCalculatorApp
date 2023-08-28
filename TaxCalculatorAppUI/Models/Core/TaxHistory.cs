namespace TaxCalculatorAppUI.Models.Core
{
    public class TaxHistory
    {
        #region Properties

        public int Id { get; set; }
        public decimal AnnualIncome { get; set; }
        public decimal TaxAmount { get; set; }
        public string PostalCode { get; set; }
        public DateTime Created { get; set; }

        #endregion
    }
}

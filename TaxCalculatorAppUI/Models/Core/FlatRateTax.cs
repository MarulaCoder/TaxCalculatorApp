namespace TaxCalculatorAppUI.Models.Core
{
    public class FlatRateTax
    {
        #region Properties

        public int Id { get; set; }
        public string TaxType { get; set; }
        public decimal Rate { get; set; }

        #endregion
    }
}

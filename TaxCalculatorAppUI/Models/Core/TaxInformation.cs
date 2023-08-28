namespace TaxCalculatorAppUI.Models.Core
{
    public class TaxInformation
    {
        #region Properties

        public FlatValueTax FlatValueTax { get; set; }
        public FlatRateTax FlatRateTax { get; set; }
        public IEnumerable<ProgressiveTax> ProgressiveTax { get; set; }

        #endregion
    }
}

namespace TaxCalculatorApp.Models.Core
{
    public class TaxInformation
    {
        #region Properties

        public FlatValueTax FlatValue { get; set; }
        public FlatRateTax FlatRate { get; set; }
        public IEnumerable<ProgressiveTax> ProgressiveTax { get; set; }

        #endregion
    }
}

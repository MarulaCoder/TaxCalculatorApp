using TaxCalculator.Domain.Core.Enums;

namespace TaxCalculatorAppUI.Models.Core
{
    public class FlatValueTax
    {
        #region Properties

        public TaxTypeEnum TaxType { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Threshold { get; set; }
        public decimal ThresholdRate { get; set; }

        #endregion
    }
}

using TaxCalculator.Domain.Core.Enums;

namespace TaxCalculatorAppUI.Models.Core
{
    public class ProgressiveTax
    {
        #region Properties

        public decimal Rate { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public TaxLevelEnum Level { get; set; }

        #endregion
    }
}

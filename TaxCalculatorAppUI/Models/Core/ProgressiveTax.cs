using TaxCalculator.Domain.Core.Enums;

namespace TaxCalculatorAppUI.Models.Core
{
    public class ProgressiveTax
    {
        #region Properties

        public int Id { get; set; }
        public string TaxType { get; set; }
        public decimal Rate { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public string AdditionalInformation { get; set; }

        #endregion
    }
}

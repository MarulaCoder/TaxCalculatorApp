using TaxCalculator.Domain.Core.Shared;

namespace TaxCalculator.Domain.Core.Entities
{
    public class ProgressiveTax : IEntity
    {
        #region Constructors

        private ProgressiveTax(decimal rate, decimal minValue, decimal maxValue, string additionalInformation) 
        { 
            Rate = rate;
            MinValue = minValue;
            MaxValue = maxValue;
            AdditionalInformation = additionalInformation;
        }

        #endregion

        #region Properties

        public int Id { get; private set; }
        public decimal Rate { get; private set; }
        public decimal MinValue { get; private set; }
        public decimal MaxValue { get; private set; }
        public string AdditionalInformation { get; private set; }

        #endregion

        #region Public Methods

        public static ProgressiveTax Create(decimal rate, decimal minValue, decimal maxValue, string info) 
        { 
            return new ProgressiveTax(rate, minValue, maxValue, info);
        }

        public void Update(decimal rate, decimal minValue, decimal maxValue)
        {
            Rate = rate;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        #endregion
    }
}

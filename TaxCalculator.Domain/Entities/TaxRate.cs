using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Domain.Core.Enums;
using TaxCalculator.Domain.Core.Shared;

namespace TaxCalculator.Domain.Core.Entities
{
    public class TaxRate : IEntity
    {
        #region Constructors

        private TaxRate(decimal rate, decimal minValue, decimal maxValue, TaxLevelEnum taxLevel) 
        { 
            Rate = rate;
            MinValue = minValue;
            MaxValue = maxValue;
            TaxLevel = taxLevel;
        }

        #endregion

        #region Properties

        public int Id { get; private set; }
        public decimal Rate { get; private set; }
        public decimal MinValue { get; private set; }
        public decimal MaxValue { get; private set; }
        public TaxLevelEnum TaxLevel { get; private set; }

        #endregion

        #region Public Methods

        public static TaxRate Create(decimal rate, decimal minValue, decimal maxValue, TaxLevelEnum taxLevel) 
        { 
            return new TaxRate(rate, minValue, maxValue, taxLevel);
        }

        public void Update(decimal rate, decimal minValue, decimal maxValue, TaxLevelEnum taxLevel)
        {
            Rate = rate;
            MinValue = minValue;
            MaxValue = maxValue;
            TaxLevel = taxLevel;
        }

        #endregion
    }
}

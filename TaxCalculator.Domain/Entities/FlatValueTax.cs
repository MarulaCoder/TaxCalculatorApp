using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Domain.Core.Shared;

namespace TaxCalculator.Domain.Core.Entities
{
    public class FlatValueTax : IEntity
    {
        #region Constructors

        private FlatValueTax(decimal flatValue, decimal threshold, decimal thresholdRate) 
        { 
            FlatValue = flatValue;
            Threshold = threshold;
            ThresholdRate = thresholdRate;
        }

        #endregion

        #region Properties

        public int Id { get; private set; }
        public decimal FlatValue { get; private set; }
        public decimal Threshold { get; private set; }
        public decimal ThresholdRate { get; private set; }

        #endregion

        #region Public Methods

        public static FlatValueTax Create(decimal flatValue, decimal threshold, decimal thresholdRate)
        { 
            return new FlatValueTax(flatValue, threshold, thresholdRate);
        }

        public void Update(decimal flatValue, decimal threshold, decimal thresholdRate)
        {
            FlatValue = flatValue;
            Threshold = threshold;
            ThresholdRate = thresholdRate;
        }

        #endregion
    }
}

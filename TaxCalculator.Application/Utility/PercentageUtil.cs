using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Application.Utility
{
    public static class PercentageUtil
    {
        #region Public Methods

        public static decimal GetPercentage(decimal value)
        {
            return value / 100;
        }

        #endregion
    }
}

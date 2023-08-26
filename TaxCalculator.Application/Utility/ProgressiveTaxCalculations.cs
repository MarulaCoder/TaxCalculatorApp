using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Domain.Core.Entities;

namespace TaxCalculator.Application.Utility
{
    public static class ProgressiveTaxCalculations
    {
        #region Public Methods

        public static decimal PerformTaxCalculation(decimal annualIncome, TaxRate taxRate)
        {
            return annualIncome - (annualIncome * (taxRate.Rate / 100));
        }



        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Application.Models.Response
{
    public class ProgressiveTaxResponse
    {
        #region Properties

        public decimal TaxAmount { get; set; }
        public string TaxDescription { get; set; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Application.Models.Requests
{
    public class CalculateTaxRequest
    {
        #region Properties

        public decimal AnnualIncome { get; set; }
        public string PostalCode { get; set; }

        #endregion
    }
}

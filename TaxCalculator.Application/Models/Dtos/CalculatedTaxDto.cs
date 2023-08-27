using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Application.Models.Dtos
{
    public class CalculatedTaxDto
    {
        #region Properties

        public int Id { get; set; }
        public decimal AnnualIncome { get; set; }
        public decimal TaxAmount { get; set; }
        public string PostalCode { get; set; }
        public DateTime Created { get; set; }

        #endregion
    }
}

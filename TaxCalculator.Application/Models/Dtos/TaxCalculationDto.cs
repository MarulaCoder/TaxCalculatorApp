using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Domain.Core.Enums;

namespace TaxCalculator.Application.Models.Dtos
{
    public class TaxCalculationDto
    {
        #region Properties

        public decimal AnnualIncome { get; set; }
        public TaxMethodDto TaxMethod { get; set; }
        public decimal IncomeAfterTax { get; set; }
        public decimal TaxAmount { get; set; }

        #endregion
    }
}

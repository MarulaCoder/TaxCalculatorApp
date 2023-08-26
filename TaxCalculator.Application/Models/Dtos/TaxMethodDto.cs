using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Domain.Core.Enums;

namespace TaxCalculator.Application.Models.Dtos
{
    public class TaxMethodDto
    {
        #region Properties

        public TaxTypeEnum TaxType { get; set; }
        public string TaxDescription { get; set; }

        // marginal rate expressed as the rate on each additional unit of income
        public decimal MarginalTaxRate { get; set; }

        //the effective (average) rate expressed as the total tax paid divided by total income
        public decimal AverageTaxRate { get; set; }


        #endregion
    }
}

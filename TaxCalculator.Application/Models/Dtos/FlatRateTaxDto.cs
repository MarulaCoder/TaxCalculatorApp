using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Domain.Core.Enums;

namespace TaxCalculator.Application.Models.Dtos
{
    public class FlatRateTaxDto
    {
        #region Properties

        public TaxTypeEnum TaxType { get; set; }
        public decimal? Rate { get; set; }

        #endregion
    }
}

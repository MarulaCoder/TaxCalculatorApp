using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Domain.Core.Enums;

namespace TaxCalculator.Application.Models.Dtos
{
    public class TaxInformationDto
    {
        #region Properties

        public FlatValueTaxDto FlatValueTax { get; set; }
        public FlatRateTaxDto FlatRateTax { get; set; }
        public IEnumerable<ProgressiveTaxDto> ProgressiveTax { get; set; }

        #endregion
    }
}

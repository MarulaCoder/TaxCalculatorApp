using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Domain.Core.Enums;

namespace TaxCalculator.Application.Models.Dtos
{
    public class UpdateProgressiveTaxDto
    {
        #region Properties

        public int Id { get; set; }
        public decimal Rate { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }

        #endregion
    }
}

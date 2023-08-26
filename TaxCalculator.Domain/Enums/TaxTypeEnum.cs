using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Domain.Core.Enums
{
    public enum TaxTypeEnum
    {
        [Description("Progressive")]
        Progressive,

        [Description("Flat Rate")]
        FlatRate,

        [Description("Flat Value")]
        FlatValue
    }
}

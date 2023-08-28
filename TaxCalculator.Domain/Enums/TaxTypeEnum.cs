using System.ComponentModel;

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

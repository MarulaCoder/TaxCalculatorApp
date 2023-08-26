using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Domain.Core.Enums
{
    public enum TaxLevelEnum
    {
        [Description("Tax Level 1")]
        LevelOne = 1,

        [Description("Tax Level 2")]
        LevelTwo = 2,

        [Description("Tax Level 3")]
        LevelThree = 3,

        [Description("Tax Level 4")]
        LevelFour = 4,

        [Description("Tax Level 5")]
        LevelFive = 5,

        [Description("Tax Level 6")]
        LevelSix = 6
    }
}

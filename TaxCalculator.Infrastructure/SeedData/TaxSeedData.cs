using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Domain.Core.Entities;
using TaxCalculator.Domain.Core.Enums;

namespace TaxCalculator.Infrastructure.SeedData
{
    public class TaxSeedData
    {
        public static List<TaxType> GetTaxTypes() 
        {
            return new List<TaxType>() 
            { 
                TaxType.Create("7441", TaxTypeEnum.Progressive),
                TaxType.Create("A100", TaxTypeEnum.FlatValue),
                TaxType.Create("7000", TaxTypeEnum.FlatRate),
                TaxType.Create("1000", TaxTypeEnum.Progressive)
            };
        }

        public static List<TaxRate> GetTaxRates() 
        {
            return new List<TaxRate>()
            {
                TaxRate.Create(10, 0, 8350),
                TaxRate.Create(15, 8351, 33950),
                TaxRate.Create(25, 33951, 82250),
                TaxRate.Create(28, 82251, 171550),
                TaxRate.Create(33, 171551, 372950),
                TaxRate.Create(35, 372951, 8350)
            };
        }

        public static FlatValueTax GetFlatValueTax() 
        {
            return FlatValueTax.Create(10000, 200000, 5);
        }
    }
}

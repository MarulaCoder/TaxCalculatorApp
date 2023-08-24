using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Domain.Core.Entities;

namespace TaxCalculator.Infrastructure.SeedData
{
    public class TaxSeedData
    {
        public static List<TaxType> GetTaxTypes() 
        {
            return new List<TaxType>() 
            { 
                TaxType.Create("7441", "Progressive"),
                TaxType.Create("A100", "FlatValue"),
                TaxType.Create("7000", "FlatRate"),
                TaxType.Create("1000", "Progressive"),
            };
        }
    }
}

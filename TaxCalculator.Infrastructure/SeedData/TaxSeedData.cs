using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Domain.Core.Entities;
using TaxCalculator.Domain.Core.Enums;
using TaxCalculator.Infrastructure.Context;

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

        public static List<ProgressiveTax> GetProgressiveTaxRates() 
        {
            return new List<ProgressiveTax>()
            {
                ProgressiveTax.Create(10M, 0M, 8350M, string.Empty),
                ProgressiveTax.Create(15M, 8351M, 33950M, "0 to 8 350 at 10%"),
                ProgressiveTax.Create(25M, 33951M, 82250M, "8 351 to 33 950 at 15%"),
                ProgressiveTax.Create(28M, 82251M, 171550M, "33 951 to 82 250 at 25%"),
                ProgressiveTax.Create(33M, 171551M, 372950M, "82 251 to 171 550 at 28%"),
                ProgressiveTax.Create(35M, 372951M, 0M, "171 551 to 372 950 at 33%")
            };
        }

        public static FlatValueTax GetFlatValueTax() 
        {
            return FlatValueTax.Create(10000M, 200000M, 5M);
        }

        public static FlatRateTax GetFlatRateTax()
        {
            return FlatRateTax.Create(17.5M);
        }

        public static void InitializeData(IServiceProvider serviceProvider)
        {            
            using var _dbContext = serviceProvider.GetRequiredService<AppDbContext>();
            _dbContext.Database.EnsureCreated();
            _dbContext.Database.Migrate();

            if (!_dbContext.ProgressiveTaxRates.Any())
            {
                var taxRates = GetProgressiveTaxRates();
                _dbContext.ProgressiveTaxRates.AddRange(taxRates);
            }

            if (!_dbContext.TaxTypes.Any())
            {
                var taxTypes = GetTaxTypes();
                _dbContext.TaxTypes.AddRange(taxTypes);
            }

            if (_dbContext.FlatValueTax.FirstOrDefault() == null)
            {
                var flatValueTax = GetFlatValueTax();
                _dbContext.FlatValueTax.Add(flatValueTax);
            }

            if (_dbContext.FlatRateTax.FirstOrDefault() == null)
            {
                var flatRateTax = GetFlatRateTax();
                _dbContext.FlatRateTax.Add(flatRateTax);
            }

            _dbContext.SaveChanges();
        }
    }
}

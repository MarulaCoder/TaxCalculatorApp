using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static List<TaxRate> GetTaxRates() 
        {
            return new List<TaxRate>()
            {
                TaxRate.Create(10M, 0M, 8350M, TaxLevelEnum.LevelOne),
                TaxRate.Create(15M, 8351M, 33950M, TaxLevelEnum.LevelTwo),
                TaxRate.Create(25M, 33951M, 82250M, TaxLevelEnum.LevelThree),
                TaxRate.Create(28M, 82251M, 171550M, TaxLevelEnum.LevelFour),
                TaxRate.Create(33M, 171551M, 372950M, TaxLevelEnum.LevelThree),
                TaxRate.Create(35M, 372951M, 0M, TaxLevelEnum.LevelSix)
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

            // Look for any movies.
            if (!_dbContext.TaxRates.Any())
            {
                var taxRates = GetTaxRates();
                _dbContext.TaxRates.AddRange(taxRates);
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

        public static void ResetSeedData(IServiceProvider serviceProvider)
        {
            using var _dbContext = serviceProvider.GetRequiredService<AppDbContext>();
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            _dbContext.Database.Migrate();
        }
    }
}

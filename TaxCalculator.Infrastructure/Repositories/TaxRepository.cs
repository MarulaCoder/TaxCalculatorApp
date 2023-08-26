using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Domain.Core.Entities;
using TaxCalculator.Domain.Core.Repositories;
using TaxCalculator.Infrastructure.Context;

namespace TaxCalculator.Infrastructure.Repositories
{
    public class TaxRepository : Repository, ITaxRepository
    {
        #region Fields

        private readonly AppDbContext _dbContext;

        #endregion

        #region Constructors

        public TaxRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Public Methods

        public async Task<TaxType> GetTaxType(string code, CancellationToken cancellationToken)
        {
            return await _dbContext.TaxTypes.FirstOrDefaultAsync(x => x.Code == code);
        }

        public async Task<TaxRate> GetTaxRateByIncome(decimal annualIncome, CancellationToken cancellationToken)
        {
            return await _dbContext.TaxRates.FirstOrDefaultAsync(x => x.MinValue >= annualIncome && x.MaxValue <= annualIncome);
        }

        public async Task<List<TaxRate>> GetAllTaxRates(CancellationToken cancellationToken)
        { 
            return await _dbContext.TaxRates.ToListAsync();
        }

        public async Task<FlatValueTax> GetFlatValueTax(CancellationToken cancellationToken)
        {
            return await _dbContext.FlatValueTax.FirstOrDefaultAsync();
        }

        public async Task<FlatRateTax> GetFlatRateTax(CancellationToken cancellationToken)
        { 
            return await _dbContext.FlatRateTax.FirstOrDefaultAsync();
        }

        public async Task PersistTaxCalculation(CalculatedTax calculatedTax, CancellationToken cancellationToken)
        { 
            await AddAsync(calculatedTax, cancellationToken);
            await _dbContext.SaveChangesAsync();
        }

        #endregion

        #region Private Methods
        #endregion
    }
}

using Microsoft.EntityFrameworkCore;
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

        public async Task<ProgressiveTax> GetProgressiveTaxRateByIncome(decimal annualIncome, CancellationToken cancellationToken)
        {
            return await _dbContext.ProgressiveTaxRates.FirstOrDefaultAsync(x => x.MinValue >= annualIncome && x.MaxValue <= annualIncome);
        }

        public async Task<List<ProgressiveTax>> GetProgressiveTaxRates(CancellationToken cancellationToken)
        { 
            var result = await _dbContext.ProgressiveTaxRates.ToListAsync();
            return result;
        }

        public async Task<FlatValueTax> GetFlatValueTax(CancellationToken cancellationToken)
        {
            var result = await _dbContext.FlatValueTax.FirstOrDefaultAsync();
            return result;
        }

        public async Task<FlatRateTax> GetFlatRateTax(CancellationToken cancellationToken)
        {
            var result = await _dbContext.FlatRateTax.FirstOrDefaultAsync();
            return result;
        }

        public async Task PersistTaxCalculation(CalculatedTax calculatedTax, CancellationToken cancellationToken)
        { 
            await AddAsync(calculatedTax, cancellationToken);
            await _dbContext.SaveChangesAsync();
        }

        #endregion
    }
}

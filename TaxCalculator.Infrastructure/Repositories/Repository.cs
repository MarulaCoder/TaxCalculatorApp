using Microsoft.EntityFrameworkCore;
using TaxCalculator.Domain.Core.Repositories;
using TaxCalculator.Infrastructure.Context;

namespace TaxCalculator.Infrastructure.Repositories
{
    public class Repository : IRepository
    {
        #region Properties

        private readonly AppDbContext _context;

        #endregion

        #region Constructors

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Public Methods

        public async Task<TEntity> GetByIdAsync<TEntity>(int id, CancellationToken cancellationToken) where TEntity : class
        {
            return await _context.Set<TEntity>().FindAsync(id, cancellationToken);
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class
        {
            var result = await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return result.Entity;
        }

        public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class
        {
            var result = _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return result.Entity;
        }

        public async Task DeleteAsync<TEntity>(int id, CancellationToken cancellationToken) where TEntity : class
        {
            var entity = await GetByIdAsync<TEntity>(id, cancellationToken);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        #endregion
    }
}

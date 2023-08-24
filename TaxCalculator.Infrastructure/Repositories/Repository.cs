using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Domain.Core.Repositories;
using TaxCalculator.Domain.Core.Shared;
using TaxCalculator.Infrastructure.Context;

namespace TaxCalculator.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        #region Properties

        private readonly AppDbContext _context;
        private DbSet<TEntity> _dbSet;

        #endregion

        #region Constructors

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        #endregion

        #region Public Methods

        public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbSet.FindAsync(id, cancellationToken);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var result = await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return result.Entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var result = _dbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return result.Entity;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        #endregion
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Domain.Core.Shared;
using TaxCalculator.Infrastructure.Context;

namespace TaxCalculator.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        #region Properties

        private readonly AppDbContext _context;

        #endregion

        public Repository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var result = _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();

            return result.Entity;
        }
    }
}

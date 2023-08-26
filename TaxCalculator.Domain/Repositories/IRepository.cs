using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Domain.Core.Repositories
{
    public interface IRepository
    {
        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class;

        Task<TEntity> GetByIdAsync<TEntity>(int id, CancellationToken cancellationToken) where TEntity : class;

        Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class;

        Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class;

        Task DeleteAsync<TEntity>(int id, CancellationToken cancellationToken) where TEntity : class;
    }
}

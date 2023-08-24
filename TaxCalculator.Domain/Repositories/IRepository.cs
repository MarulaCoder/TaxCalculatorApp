using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Domain.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);

        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}

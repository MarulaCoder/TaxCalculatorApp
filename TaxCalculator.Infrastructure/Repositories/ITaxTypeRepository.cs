using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Domain.Core.Entities;

namespace TaxCalculator.Infrastructure.Repositories
{
    public interface ITaxTypeRepository : IRepository<TaxType>
    {
        TaxType GetTaxType(string code, CancellationToken cancellationToken);
    }
}

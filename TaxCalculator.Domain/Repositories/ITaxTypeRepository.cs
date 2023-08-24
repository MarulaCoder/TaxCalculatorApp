using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Domain.Core.Entities;

namespace TaxCalculator.Domain.Core.Repositories
{
    public interface ITaxTypeRepository : IRepository<TaxType>
    {
        TaxType GetTaxType(string code);
    }
}

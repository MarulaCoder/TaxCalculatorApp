using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Domain.Core.Entities;
using TaxCalculator.Infrastructure.Context;

namespace TaxCalculator.Infrastructure.Repositories
{
    public class TaxTypeRepository : Repository<TaxType>, ITaxTypeRepository
    {
        #region Fields


        #endregion

        #region Constructors

        public TaxTypeRepository(AppDbContext context) : base(context)
        {
        }

        #endregion

        #region Public Methods

        public TaxType GetTaxType(string code, CancellationToken cancellationToken)
        {
            var entity = GetAll().FirstOrDefault(x => x.Code == code);

            return entity;
        }

        #endregion

        #region Private Methods
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Application.Models.Requests;
using TaxCalculator.Domain.Core.Shared;

namespace TaxCalculator.Application.Services
{
    public class TaxCalculatorService : ITaxCalculatorService
    {
        #region Fields

        #endregion

        #region Constructors
        #endregion

        #region Public Methods

        public Task<Result> CalculateTax(CalculateTaxRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods
        #endregion
    }
}

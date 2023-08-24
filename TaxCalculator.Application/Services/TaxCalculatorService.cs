using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Application.Models.Requests;
using TaxCalculator.Domain.Core.Repositories;
using TaxCalculator.Domain.Core.Shared;

namespace TaxCalculator.Application.Services
{
    public class TaxCalculatorService : ITaxCalculatorService
    {
        #region Fields

        private readonly ITaxTypeRepository _taxTypeRepository;

        #endregion

        #region Constructors

        public TaxCalculatorService(ITaxTypeRepository taxTypeRepository)
        { 
            _taxTypeRepository = taxTypeRepository ?? throw new ArgumentNullException(nameof(taxTypeRepository));
        }

        #endregion

        #region Public Methods

        public Task<Result> CalculateTax(CalculateTaxRequest request, CancellationToken cancellationToken)
        {
            var result = Result.Failure("Initilization");

            var response = _taxTypeRepository.GetTaxType(request.PostalCode);
            if (response == null) 
            {
                result = Result.Failure("Tax type not found");
            }

            return Task.FromResult(result);
        }

        #endregion

        #region Private Methods
        #endregion
    }
}

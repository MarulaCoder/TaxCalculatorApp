﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Domain.Core.Entities;

namespace TaxCalculator.Domain.Core.Repositories
{
    public interface ITaxRepository : IRepository
    {
        Task<TaxType> GetTaxType(string code, CancellationToken cancellationToken);

        Task<TaxRate> GetTaxRateByIncome(decimal annualIncome, CancellationToken cancellationToken);

        Task<List<TaxRate>> GetAllTaxRates(CancellationToken cancellationToken);

        Task<FlatValueTax> GetFlatValueTax(CancellationToken cancellationToken);

        Task<FlatRateTax> GetFlatRateTax(CancellationToken cancellationToken);

        Task PersistTaxCalculation(CalculatedTax calculatedTax, CancellationToken cancellationToken);
    }
}
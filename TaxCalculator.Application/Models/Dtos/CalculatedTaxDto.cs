using AutoMapper;
using TaxCalculator.Application.Mapping;
using TaxCalculator.Domain.Core.Entities;

namespace TaxCalculator.Application.Models.Dtos
{
    public class CalculatedTaxDto : IMapFrom<CalculatedTax>
    {
        #region Properties

        public int Id { get; set; }
        public decimal AnnualIncome { get; set; }
        public decimal TaxAmount { get; set; }
        public string PostalCode { get; set; }
        public DateTime Created { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CalculatedTax, CalculatedTaxDto>();
        }

        #endregion
    }
}

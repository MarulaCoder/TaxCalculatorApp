using AutoMapper;
using TaxCalculator.Application.Mapping;
using TaxCalculator.Domain.Core.Entities;
using TaxCalculator.Domain.Core.Enums;

namespace TaxCalculator.Application.Models.Dtos
{
    public class FlatRateTaxDto : IMapFrom<FlatRateTax>
    {
        #region Properties

        public int Id { get; set; }
        public TaxTypeEnum TaxType { get; set; }
        public decimal? Rate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FlatRateTax, FlatRateTaxDto>();
        }

        #endregion
    }
}

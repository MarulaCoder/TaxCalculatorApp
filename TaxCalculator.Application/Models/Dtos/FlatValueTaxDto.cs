using AutoMapper;
using TaxCalculator.Application.Mapping;
using TaxCalculator.Domain.Core.Entities;
using TaxCalculator.Domain.Core.Enums;

namespace TaxCalculator.Application.Models.Dtos
{
    public class FlatValueTaxDto : IMapFrom<FlatValueTax>
    {
        #region Properties

        public int Id { get; set; }
        public TaxTypeEnum TaxType { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? Threshold { get; set; }
        public decimal? ThresholdRate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FlatValueTax, FlatValueTaxDto>();
        }

        #endregion
    }
}

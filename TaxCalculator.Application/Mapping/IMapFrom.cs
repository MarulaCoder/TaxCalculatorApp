using AutoMapper;

namespace TaxCalculator.Application.Mapping
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile);
    }
}

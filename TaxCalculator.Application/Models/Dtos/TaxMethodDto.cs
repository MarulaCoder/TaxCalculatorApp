using TaxCalculator.Domain.Core.Enums;

namespace TaxCalculator.Application.Models.Dtos
{
    public class TaxMethodDto
    {
        #region Properties

        public TaxTypeEnum TaxType { get; set; }
        public string TaxDescription { get; set; }

        #endregion
    }
}

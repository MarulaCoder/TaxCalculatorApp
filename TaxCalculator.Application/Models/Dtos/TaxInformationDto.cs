namespace TaxCalculator.Application.Models.Dtos
{
    public class TaxInformationDto
    {
        #region Properties

        public FlatValueTaxDto FlatValueTax { get; set; }
        public FlatRateTaxDto FlatRateTax { get; set; }
        public IEnumerable<ProgressiveTaxDto> ProgressiveTax { get; set; }

        #endregion
    }
}

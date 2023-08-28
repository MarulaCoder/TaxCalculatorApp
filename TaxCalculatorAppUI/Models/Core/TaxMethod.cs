namespace TaxCalculatorAppUI.Models.Core
{
    public class TaxMethod
    {
        #region Properties

        public string TaxType { get; set; }
        public string TaxDescription { get; set; }

        // marginal rate expressed as the rate on each additional unit of income
        //public decimal MarginalTaxRate { get; set; }

        //the effective (average) rate expressed as the total tax paid divided by total income
        //public decimal AverageTaxRate { get; set; }

        #endregion
    }
}

using TaxCalculator.Domain.Core.Shared;

namespace TaxCalculator.Domain.Core.Entities
{
    public class CalculatedTax : IEntity
    {
        #region Constructors

        private CalculatedTax(decimal annualIncome, decimal taxAmount, string postalCode) 
        { 
            AnnualIncome = annualIncome;
            TaxAmount = taxAmount;
            PostalCode = postalCode;
            Created = DateTime.UtcNow;
        }

        #endregion

        #region Properties

        public int Id { get; private set; } 
        public decimal AnnualIncome { get; private set; }
        public decimal TaxAmount { get; private set; }
        public string PostalCode { get; private set; }
        public DateTime Created { get; private set; }

        #endregion

        #region Public Methods

        public static CalculatedTax Create(decimal annualIncome, decimal taxAmount, string postalCode)
        {
            return new CalculatedTax(annualIncome, taxAmount, postalCode);
        }

        #endregion
    }
}

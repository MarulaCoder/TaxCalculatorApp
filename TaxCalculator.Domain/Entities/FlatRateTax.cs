using TaxCalculator.Domain.Core.Shared;

namespace TaxCalculator.Domain.Core.Entities
{
    public class FlatRateTax : IEntity
    {
        #region Constructors

        private FlatRateTax(decimal flatRate)
        { 
            FlatRate = flatRate;
        }

        #endregion

        #region Properties

        public int Id { get; private set; }
        public decimal FlatRate { get; private set; }

        #endregion

        #region Public Methods

        public static FlatRateTax Create(decimal flatRate) 
        { 
            return new FlatRateTax(flatRate);
        }

        public void Update(decimal flatRate)
        {
            FlatRate = flatRate;
        }

        #endregion
    }
}

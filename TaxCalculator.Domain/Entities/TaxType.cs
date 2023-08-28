using TaxCalculator.Domain.Core.Enums;
using TaxCalculator.Domain.Core.Shared;

namespace TaxCalculator.Domain.Core.Entities
{
    public class TaxType : IEntity
    {
        #region Constructors

        private TaxType(string code, TaxTypeEnum type) 
        {
            Code = code;
            Type = type;
        }

        #endregion

        #region Properties

        public int Id { get; private set; }
        public string Code { get; private set; }
        public TaxTypeEnum Type { get; private set; }

        #endregion

        #region Public Methods

        public static TaxType Create(string code, TaxTypeEnum type) 
        { 
            return new TaxType(code, type);
        }

        public void Update(string code, TaxTypeEnum type)
        {
            Code = code;
            Type = type;
        }

        #endregion
    }
}

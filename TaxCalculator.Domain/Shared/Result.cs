using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Domain.Core.Shared
{
    public class Result
    {
        #region Constructors

        private Result(bool isSuccessful, string errorMessage) 
        {
            if (!isSuccessful && string.IsNullOrWhiteSpace(errorMessage))
            { 
                throw new InvalidOperationException("Error message cannot be an empty value.");
            }

            IsSuccessful = isSuccessful;
            ErrorMessage = errorMessage;
        }

        #endregion

        #region Properties

        public bool IsSuccessful { get; }
        public bool IsFailure => !IsSuccessful;
        public string ErrorMessage { get; }

        #endregion

        #region Public Methods

        public static Result Success() => new Result(true, string.Empty);

        public static Result Failure(string errorMessage) => new Result(false, errorMessage);

        #endregion
    }
}

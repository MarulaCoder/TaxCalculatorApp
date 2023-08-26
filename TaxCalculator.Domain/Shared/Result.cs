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

        protected Result(bool isSuccessful, string errorMessage) 
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

        public static Result<TData> Success<TData>(TData data) => new Result<TData>(data, true, string.Empty);

        public static Result<TData> Failure<TData>(string errorMessage) => new Result<TData>(default, false, errorMessage);

        #endregion
    }

    public class Result<TData> : Result
    {
        #region Fields

        private readonly TData _data;

        #endregion

        #region Constructors

        protected internal Result(TData data, bool isSuccessful, string errorMessage)
            : base(isSuccessful, errorMessage)
        { 
            _data = data;
        }

        #endregion

        #region Properties

        public TData Data { get { if (IsSuccessful) { return _data; } throw new InvalidOperationException("The data of a failure result cannot be accessed."); } }

        #endregion
    }
}

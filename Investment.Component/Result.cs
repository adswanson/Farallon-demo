using System;
using System.Threading.Tasks;

namespace Investment.Component
{
    /// <summary>
    /// Monad-lite construct for capturing the result of an operation
    /// </summary>
    /// <typeparam name="TValue">Value return type of the operation</typeparam>
    public sealed class Result<TValue>
    {
        public TValue Value { get; private set; }
        public bool IsSuccess { get; private set; }
        public Exception Exception { get; private set; }
        public string Message { get; private set; }

        private Result(TValue value, bool isSuccess, string message, Exception e)
        {
            Value = value;
            IsSuccess = isSuccess;
            Exception = e;
            Message = message;
        }

        /// <summary>
        /// Unwraps an operation into its value 
        /// </summary>
        public TValue Unwrap() => Value;

        /// <summary>
        /// Represents a successful operation
        /// </summary>
        public static Result<TValue> Success(TValue value)
        {
            return new Result<TValue>(value, true, null, null);
        }

        /// <summary>
        /// Represents an operation that resulted in a non-error failure state
        /// </summary>
        public static Result<TValue> Failure(string message)
        {
            return new Result<TValue>(default(TValue), false, message, null);
        }

        /// <summary>
        /// Represents an operation that resulted in an error
        /// </summary>
        public static Result<TValue> Error(Exception e, string message = null)
        {
            return new Result<TValue>(default(TValue), false, message ?? e.Message, e);
        }

        /// <summary>
        /// Wraps an operation in a try-catch and returns either <seealso cref="Success(TValue)"/> or <seealso cref="Failure(string)"/>
        /// </summary>
        public static Result<TValue> Try(Func<TValue> run)
        {
            try
            {
                return Success(run());
            }
            catch (Exception e)
            {
                return Error(e, e.Message);
            }
        }

        /// <inheritdoc cref="Try(Func{TValue})"/>
        public static async Task<Result<TValue>> TryAsync(Func<Task<TValue>> run)
        {
            try
            {
                var value = await run();
                return Success(value);
            }
            catch (Exception e)
            {
                return Error(e, e.Message);
            }
        }
    }
}

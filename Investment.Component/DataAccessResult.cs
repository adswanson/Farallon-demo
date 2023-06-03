using System;

namespace Investment.Component
{
    public class DataAccessResult<TRecord>
    {
        public bool IsSuccess { get; set; }
        public TRecord Value { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }

        public static DataAccessResult<TRecord> Success(TRecord value, string message = null)
        {
            return new DataAccessResult<TRecord>
            {
                Value = value,
                IsSuccess = true,
                Exception = null,
                Message = message
            };
        }

        public static DataAccessResult<TRecord> Failure(string message)
        {
            return new DataAccessResult<TRecord>
            {
                Value = default,
                IsSuccess = false,
                Exception = null,
                Message = message
            };
        }

        public static DataAccessResult<TRecord> Error(Exception exception, string message = null)
        {
            return new DataAccessResult<TRecord>
            {
                Value = default,
                IsSuccess = false,
                Exception = exception,
                Message = message ?? exception.Message
            };
        }
    }
}

using System.Net;

namespace Activities.Models.Shared
{
    public class Result<T>
    {
        public T? Payload { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public bool Failed { get; private set; }
        public bool Succeded => !Failed;
        public Exception? Exception { get; set; }

        public static Result<T> Fail()
        {
            return new Result<T>
            {
                Failed = true,
            };
        }

        public static Result<T> Success(T? payload)
        {
            return new Result<T>
            {
                Failed = false,
                Payload = payload,
            };
        }
    }

    public class Result
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public bool Failed { get; private set; }
        public bool Succeded => !Failed;
        public Exception? Exception { get; set; }

        public static Result Fail()
        {
            return new Result
            {
                Failed = true,
            };
        }

        public static Result Success()
        {
            return new Result
            {
                Failed = false,
            };
        }
    }

    public static class ResultExtensions
    {
        public static Result<T> WithError<T>(this Result<T> result, string error)
        {
            result.ErrorMessage = error;
            return result;
        }

        public static Result<T> WithException<T>(this Result<T> result, Exception? ex)
        {
            result.Exception = ex;
            return result;
        }

        public static Result WithError(this Result result, string error)
        {
            result.ErrorMessage = error;
            return result;
        }

        public static Result WithException(this Result result, Exception? ex)
        {
            result.Exception = ex;
            return result;
        }
    }
}

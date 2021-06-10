using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Model.Model
{
    public class Gateway
    {
        public Guid GatewayID { get; set; }
        public GatewayType GatewayType { get; set; }
        public string BaseAddress { get; set; }
        public string GatewayAPIID { get; set; }
        public string GatewayAPIPassword { get; set; }
        public string GatewayBearerToken { get; set; }
        public string GatewayAdditional1 { get; set; }
        public string GatewayAdditional2 { get; set; }
        public decimal ProcessingFee { get; set; }
        public string GatewayName => GatewayType.ToString();
    }

    public class GatewayErrorCode
    {
        public Guid ErrorCodeID { get; set; }
        public string Response { get; set; }
        public string Meaning { get; set; }
        public string DisplayMeaning { get; set; }
        public int GatewayType { get; set; }
        public int ResponseErrorType { get; set; }
    }

    public class Result
    {
        public bool Success { get; private set; }
        public string Error { get; private set; }

        public bool Failure
        {
            get { return !Success; }
        }

        protected Result(bool success, string error)
        {
            Success = success;
            Error = error;
        }

        public static Result Fail(string message = "")
        {
            return new Result(false, message);
        }

        public static Result<T> Fail<T>(string message = "")
        {
            return new Result<T>(default(T), false, message);
        }

        public static Result Ok()
        {
            return new Result(true, String.Empty);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, String.Empty);
        }

        public static Result Combine(params Result[] results)
        {
            foreach (Result result in results)
            {
                if (result.Failure)
                    return result;
            }

            return Ok();
        }
    }


    public class Result<T> : Result
    {
        private T _value;

        public T Value
        {
            get
            {
                return _value;
            }
            private set { _value = value; }
        }

        protected internal Result(T value, bool success, string error)
            : base(success, error)
        {
            Value = value;
        }
    }

    public class ErrorModel
    {
        public string Message { get; set; }
        public int Code { get; set; }
    }
}

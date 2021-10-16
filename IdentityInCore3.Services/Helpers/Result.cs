using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityInCore3.Services.Helpers
{
    public class Result<T>
    {
        public string SuccessMessage { get; set; }

        public bool isSuccess { get; set; }
        public T ResultObject { get; set; }
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
        public string ErrorDetail { get; set; }

        public Result<T> GetResult(T resultObject)
        {
            if (resultObject == null)
            {
                ErrorCode = ErrorCodes.ResourceNotFound;
            }
            else
            {
                isSuccess = true;
                ResultObject = resultObject;
            }
            return this;
        }
    }
}

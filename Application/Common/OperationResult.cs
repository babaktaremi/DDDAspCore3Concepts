using System;

namespace Application.Common
{
   

    public class OperationResult<TResult>
    {
        public TResult Result { get; private set; }

        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }
        public bool IsException { get; set; }

        public static OperationResult<TResult> SuccessResult(TResult result)
        {
            return new OperationResult<TResult>{Result = result,IsSuccess = true};
        } 

        public static OperationResult<TResult> FailureResult(string message,TResult result=default)
        {
            return new OperationResult<TResult>{Result = result,ErrorMessage = message,IsSuccess = false};
        }
    }
}

using System;

namespace Application.Common
{
    public class OperationResult<TResult>
    {
        public TResult Result { get; private set; }

        public bool Success { get; private set; }
        public string ErrorMessage { get; private set; }
        public Exception Exception { get; private set; }

        public static OperationResult<TResult> BuildSuccessResult(TResult result)
        {
            return new OperationResult<TResult> { Success = true,Result=result };

        }

        public static OperationResult<TResult> BuildFailure(string errorMessage,TResult result)
        {
            return new OperationResult<TResult> { Success = false, ErrorMessage = errorMessage,Result = result};

        }

        public static OperationResult<TResult> BuildFailure(string errorMessage)
        {
            return new OperationResult<TResult> { Success = false, ErrorMessage = errorMessage };

        }

        public static OperationResult<TResult> BuildFailure(Exception ex)
        {
            return new OperationResult<TResult> { Success = false, Exception = ex };
        }

        public static OperationResult<TResult> BuildFailure(Exception ex, string errorMessage)
        {
            return new OperationResult<TResult> { Success = false, Exception = ex, ErrorMessage = errorMessage };
        }

    }
}

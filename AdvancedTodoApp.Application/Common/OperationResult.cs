using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.Common
{
    public class OperationResult<T>
    {
        public bool Success { get; private set; }
        public T? Data { get; private set; }
        public List<string> Errors { get; private set; } = new();
        public ErrorCode ErrorCode { get; private set; } = ErrorCode.None;

        // Private constructor so we only create instances via static factory Oka nd Fail methods
        private OperationResult(bool success, T? data, List<string> errors, ErrorCode errorCode)
        {
            Success = success;
            Data = data;
            Errors = errors ?? new List<string>(); // always return a list of string, whether we have errors or not
            ErrorCode = errorCode;
        }

        public static OperationResult<T> Ok(T data)
        {
            return new OperationResult<T>(true, data, null, ErrorCode.None);
        }

        public static OperationResult<T> Fail(List<string> errors, ErrorCode errorCode = ErrorCode.UnknownError) 
        {
            return new OperationResult<T>(false, default, errors, errorCode);
        }

        public static OperationResult<T> Fail(string error, ErrorCode errorCode = ErrorCode.UnknownError)
        {
            return new OperationResult<T>(false, default, new List<string> { error }, errorCode);
        }
    }
}

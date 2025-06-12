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

        // Private constructor so we only create instances via static factory Oka nd Fail methods
        private OperationResult(bool success, T? data, List<string> errors)
        {
            Success = success;
            Data = data;
            Errors = errors ?? new List<string>(); // always return a list of string, whether we have errors or not
        }

        public static OperationResult<T> Ok(T data)
        {
            return new OperationResult<T>(true, data, null);
        }

        public static OperationResult<T> Fail(List<string> errors) 
        {
            return new OperationResult<T>(false, default, errors);
        }

        public static OperationResult<T> Fail(string error)
        {
            return new OperationResult<T>(false, default, new List<string> { error });
        }
    }
}

using AdvancedTodoApp.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.Common
{
    public class OperationResult<T> : IOperationResult
    {
        public bool Success { get; private set; }
        public T? Data { get; private set; }
        public List<string> Errors { get; private set; } = new();
        public ErrorCode ErrorCode { get; private set; } = ErrorCode.None;
        // Add support to return different types of statuscodes (e.g. returning a 201 Created instead of 200 OK when successfully created category)
        public HttpStatusCode StatusCode { get; private set; } = HttpStatusCode.OK;

        // Private constructor so we only create instances via static factory Oka nd Fail methods
        private OperationResult(bool success, T? data, List<string> errors, ErrorCode errorCode, HttpStatusCode statusCode)
        {
            Success = success;
            Data = data;
            Errors = errors ?? new List<string>(); // always return a list of string, whether we have errors or not
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }

        // Only used in special secnarios like MediatR and FluentValidation pipeline behavior defined in ValidationBehavior.cs
        public OperationResult()
        {
            Success = false;
            Errors = new List<string>();
            ErrorCode = ErrorCode.ValidationError;
            StatusCode = HttpStatusCode.BadRequest;
        }
        // This is also only used in special scenarios(in this case, ValidationBehavior.cs in application layer)
        public void AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
            Success = false;
        }

        public static OperationResult<T> Ok(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new OperationResult<T>(true, data, null, ErrorCode.None, statusCode);
        }

        public static OperationResult<T> Fail(List<string> errors, ErrorCode errorCode = ErrorCode.UnknownError, HttpStatusCode statusCode = HttpStatusCode.BadRequest) 
        {
            return new OperationResult<T>(false, default, errors, errorCode, statusCode);
        }

        public static OperationResult<T> Fail(string error, ErrorCode errorCode = ErrorCode.UnknownError, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new OperationResult<T>(false, default, new List<string> { error }, errorCode, statusCode);
        }
    }
}

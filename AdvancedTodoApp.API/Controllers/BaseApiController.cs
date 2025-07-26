using AdvancedTodoApp.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedTodoApp.API.Controllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Helper method - maps OperationResult<T> to IActionResult(HTTP/web response model)
        /// This helps use separate concerns by letting service layer focus pureply on business logic and returns OperationResult<T>.
        /// Then the controller cleanly translates this to HTTP respones for our API
        /// 
        /// Return type is based on error code
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        // Helper method - maps OperationResult<T> to IActionResult
        protected IActionResult FromResult<T>(OperationResult<T> result)
        {
            if (result.Success)
            {
                // Returns a "Ok(result.Data)" by default, but has added support for other cases such as when returning 201 Created 
                return StatusCode((int)result.StatusCode, result.Data);
            }

            // Using ErrorCode to map to most appropriate HTTP error response.
            return result.ErrorCode switch
            {
                ErrorCode.NotFound => NotFound(result),
                ErrorCode.Unauthorized => Forbid(),
                ErrorCode.ValidationError => BadRequest(result),
                ErrorCode.Conflict => Conflict(result),
                // Fallback for unknown errors and unmapped codes - Using the custom StatusCode provided by service while including the internal error for debugging
                _ => StatusCode((int)result.StatusCode, result)
            };
        }
    }
}

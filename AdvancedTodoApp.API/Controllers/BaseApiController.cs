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
                return Ok(result.Data);
            }

            return result.ErrorCode switch
            {
                ErrorCode.NotFound => NotFound(result.Errors),
                ErrorCode.Unauthorized => Forbid(),
                ErrorCode.ValidationError => BadRequest(result.Errors),
                ErrorCode.Conflict => Conflict(result.Errors),
                _ => BadRequest(result.Errors)
            };
        }
    }
}

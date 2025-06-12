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

            return BadRequest(result.Errors);
        }
    }
}

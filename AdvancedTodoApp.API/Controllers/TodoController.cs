using AdvancedTodoApp.Application.DTOs.Todo;
using AdvancedTodoApp.Application.Features.Todos.Commands;
using AdvancedTodoApp.Application.Features.Todos.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;

namespace AdvancedTodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class TodoController : BaseApiController
    {
        private readonly IMediator _mediator;

        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Private helper method to get the Guid UserId from the users claim
        private Guid GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(userId!);
        }


        [HttpGet("user")]
        [SwaggerOperation(Summary = "Get all todos for the current user", Description = "Returns all todos associated with the currently authenticated user.")]
        [ProducesResponseType(typeof(IEnumerable<TodoDto>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var query = new GetTodosQuery { UserId = userId };
            var result = await _mediator.Send(query);
            return FromResult(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a single todo by ID", Description = "Returns a single todo by ID if it belongs to the current user.")]
        [ProducesResponseType(typeof(TodoDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = GetUserId();
            var query = new GetTodoByIdQuery { TodoId = id, UserId = userId };
            var result = await _mediator.Send(query);
            return FromResult(result);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new todo", Description = "Creates a new todo item for the currently authenticated user.")]
        [ProducesResponseType(typeof(TodoDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateTodoDto dto)
        {
            var userId = GetUserId();
            var command = new CreateTodoCommand
            {
                CreateTodoDto = dto,
                UserId = userId
            };
            var result = await _mediator.Send(command);
            return FromResult(result);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an existing todo", Description = "Updates a todo item belonging to the currently authenticated user.")]
        [ProducesResponseType(typeof(TodoDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTodoDto dto)
        {
            var userId = GetUserId();
            var command = new UpdateTodoCommand
            {
                TodoId = id,
                UpdateTodoDto = dto,
                UserId = userId
            };
            var result = await _mediator.Send(command);
            return FromResult(result);

        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a todo", Description = "Deletes a todo item belonging to the currently authenticated user.")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();
            var command = new DeleteTodoCommand 
            {
                UserId = userId,
                Id = id
            };
            var result = await _mediator.Send(command);
            return FromResult(result);
        }        
    }
}

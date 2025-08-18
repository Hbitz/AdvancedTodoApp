using AdvancedTodoApp.Application.DTOs.Todo;
using AdvancedTodoApp.Application.Features.Todos.Commands;
using AdvancedTodoApp.Application.Features.Todos.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdvancedTodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var query = new GetTodosQuery { UserId = userId };
            var result = await _mediator.Send(query);
            return FromResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = GetUserId();
            var query = new GetTodoByIdQuery { TodoId = id, UserId = userId };
            var result = await _mediator.Send(query);
            return FromResult(result);
        }

        [HttpPost]
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
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();
            var command = new DeleteTodoCommand { Id = id };
            var result = await _mediator.Send(command);
            return FromResult(result);
        }        
    }
}

using AdvancedTodoApp.Application.Interfaces.Services;
using AdvancedTodoApp.Application.DTOs.Category;
using AdvancedTodoApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AdvancedTodoApp.Application.Features.Categories.Commands;
using MediatR;
using AdvancedTodoApp.Application.Features.Categories.Queries;

namespace AdvancedTodoApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoryController : BaseApiController
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(userId!); // This works based on the assumption all tokens include user ID as guid
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = GetUserId();
            var query = new GetCategoryByIdQuery
            {
                CategoryId = id,
                UserId = userId
            };
            var result = await _mediator.Send(query);
            return FromResult(result); // Uses BaseApiController to map result to HTTP response(web response model)
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var query = new GetCategoriesQuery 
            { 
                UserId = userId
            };
            var result = await _mediator.Send(query);
            return FromResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            var userId = GetUserId();
            var command = new CreateCategoryCommand
            {
                Name = dto.Name,
                UserId = userId,
            };

            var result = await _mediator.Send(command);
            return FromResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryDto dto)
        {
            var userId = GetUserId();
            var command = new UpdateCategoryCommand
            {
                CategoryId = id,
                UserId = userId,
                Name = dto.Name,
            };
            var result = await _mediator.Send(command);
            return FromResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();
            var command = new DeleteCategoryCommand
            {
                CategoryId = id,
                UserId = userId
            };
            var result = await _mediator.Send(command);
            return FromResult(result);
        }
    }
}

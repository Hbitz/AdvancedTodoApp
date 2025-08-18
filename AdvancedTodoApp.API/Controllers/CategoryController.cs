using AdvancedTodoApp.Application.Interfaces.Services;
using AdvancedTodoApp.Application.DTOs.Category;
using AdvancedTodoApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AdvancedTodoApp.Application.Features.Categories.Commands;
using MediatR;
using AdvancedTodoApp.Application.Features.Categories.Queries;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "Get category by ID", Description = "Fetch a single category by its ID.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [SwaggerOperation(Summary = "Get all categories for user", Description = "Fetch all categories for the current authenticated user.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        [SwaggerOperation(Summary = "Create a category", Description = "Creates a new category for the authenticated user.")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [SwaggerOperation(Summary = "Update a category", Description = "Updates an existing category by ID for the authenticated user.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [SwaggerOperation(Summary = "Delete a category", Description = "Deletes a category by ID for the authenticated user.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

using AdvancedTodoApp.Application.Interfaces.Services;
using AdvancedTodoApp.Application.DTOs.Category;
using AdvancedTodoApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AdvancedTodoApp.Application.Features.Categories.Commands;
using MediatR;

namespace AdvancedTodoApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMediator _mediator;

        public CategoryController(ICategoryService categoryService, IMediator mediator)
        {
            _categoryService = categoryService;
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
            var result  = await _categoryService.GetByIdAsync(id, userId);
            return FromResult(result); // Uses BaseApiController to map result to HTTP response(web response model)
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var result = await _categoryService.GetAllByUserIdAsync(userId);
            return FromResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            var userId = GetUserId();
            //var result = await _categoryService.AddCategoryAsync(dto, userId);
            // With MediatR added, we create a command and 
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
            var result = await _categoryService.UpdateCategoryAsync(id, dto, userId);
            return FromResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();
            var result = await _categoryService.DeleteCategoryAsync(id, userId);
            return FromResult(result);
        }
    }
}

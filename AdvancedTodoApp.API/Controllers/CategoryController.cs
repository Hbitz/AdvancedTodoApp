using AdvancedTodoApp.Application.Interfaces.Services;
using AdvancedTodoApp.Application.DTOs.Category;
using AdvancedTodoApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AdvancedTodoApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(userId!); // This works based on the assumption all tokens include user ID as guid
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(Guid id)
        {
            var userId = GetUserId();
            var category = await _categoryService.GetByIdAsync(id, userId);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var categories = await _categoryService.GetAllByUserIdAsync(userId);
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            var userId = GetUserId();
            await _categoryService.AddCategoryAsync(dto, userId);
            return NoContent();
            //return CreatedAtAction(nameof(GetById), new { id = userId }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("ID doesn't match");
            }
            var userId = GetUserId();
            await _categoryService.UpdateCategoryAsync(dto, userId);
            return NoContent(); // todo - Ok() or other status instead?
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();
            await _categoryService.DeleteCategoryAsync(id, userId);
            return NoContent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedTodoApp.Application.Interfaces.Persistence;
using AdvancedTodoApp.Application.Interfaces.Services;
using AdvancedTodoApp.Domain.Entities;
using AdvancedTodoApp.Application.DTOs.Category;
using AdvancedTodoApp.Application.Common;
using System.Net;

namespace AdvancedTodoApp.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<OperationResult<CategoryDto>> GetByIdAsync(Guid id, Guid userId)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null || category.UserId != userId)
            {
                return OperationResult<CategoryDto>.Fail(
                    "Category not found or unauthorized.",
                    ErrorCode.NotFound
                );
            }

            var dto = new CategoryDto {
                Id = category.Id,
                Name = category.Name,
            };

            return OperationResult<CategoryDto>.Ok(dto);


        }

        public async Task<OperationResult<List<CategoryDto>>> GetAllByUserIdAsync(Guid userId)
        {
            var categories = await _categoryRepository.GetAllByUserIdAsync(userId);
            var dtos = categories
                .Select(c => new CategoryDto { Id = c.Id, Name = c.Name })
                .ToList();

            return OperationResult<List<CategoryDto>>.Ok(dtos);
        }

        public async Task<OperationResult<CategoryDto>> AddCategoryAsync(CreateCategoryDto dto, Guid userId)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                UserId = userId
            };

            _categoryRepository.Add(category);
            await _categoryRepository.SaveChangesAsync();

            // Manually map to DTO to be able to return a 201 CreatedAt
            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };


            // Returns a 201 Created 
            return OperationResult<CategoryDto>.Ok(categoryDto, HttpStatusCode.Created);
        }

        public async Task<OperationResult<string>> UpdateCategoryAsync(UpdateCategoryDto dto, Guid userId)
        {
            var existing = await _categoryRepository.GetByIdAsync(dto.Id);
            if (existing == null)
            {
                return OperationResult<string>.Fail("Category not found.", ErrorCode.NotFound);
            }

            if (existing.UserId != userId)
            {
                return OperationResult<string>.Fail("Unauthorized to update this category.", ErrorCode.Unauthorized);
            }

            
            existing.Name = dto.Name;
            _categoryRepository.Update(existing);
            await _categoryRepository.SaveChangesAsync();

            return OperationResult<string>.Ok("Category updated successfully.");
        }

        public async Task<OperationResult<string>> DeleteCategoryAsync(Guid id, Guid userId)
        {
            var existing = await _categoryRepository.GetByIdAsync(id);
            if (existing == null)
            {
                return OperationResult<string>.Fail("Category not found.");
            }

            if (existing.UserId != userId)
            {
                return OperationResult<string>.Fail("Unauthorized to delete this category.", ErrorCode.Unauthorized);
            }

            _categoryRepository.Delete(existing);
            await _categoryRepository.SaveChangesAsync();

            return OperationResult<string>.Ok("Category deleted successfully");
        }
    }
}

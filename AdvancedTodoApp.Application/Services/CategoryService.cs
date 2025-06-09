using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedTodoApp.Application.Interfaces.Persistence;
using AdvancedTodoApp.Application.Interfaces.Services;
using AdvancedTodoApp.Domain.Entities;
using AdvancedTodoApp.Application.DTOs.Category;

namespace AdvancedTodoApp.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDto?> GetByIdAsync(Guid id, Guid userId)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null || category.UserId != userId)
            {
                return null;
            }

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public async Task<List<CategoryDto>> GetAllByUserIdAsync(Guid userId)
        {
            var categories = await _categoryRepository.GetAllByUserIdAsync(userId);
            return categories
                .Select(c => new CategoryDto { Id = c.Id, Name = c.Name })
                .ToList();
        }

        public async Task AddCategoryAsync(CreateCategoryDto dto, Guid userId)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                UserId = userId
            };

            _categoryRepository.Add(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto dto, Guid userId)
        {
            var existing = await _categoryRepository.GetByIdAsync(dto.Id);
            if (existing == null || existing.UserId != userId)
            {
                return;
            }
            
            existing.Name = dto.Name;
            _categoryRepository.Update(existing);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Guid id, Guid userId)
        {
            var existing = await _categoryRepository.GetByIdAsync(id);
            if (existing == null || existing.UserId != userId)
            {
                return;
            }

            _categoryRepository.Delete(existing);
            await _categoryRepository.SaveChangesAsync();
        }
    }
}

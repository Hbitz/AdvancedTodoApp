using AdvancedTodoApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedTodoApp.Application.DTOs.Category;

namespace AdvancedTodoApp.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<CategoryDto?> GetByIdAsync(Guid id, Guid userId);
        Task<List<CategoryDto>> GetAllByUserIdAsync(Guid userId);
        Task AddCategoryAsync(CreateCategoryDto dto, Guid userId);
        Task UpdateCategoryAsync(UpdateCategoryDto dto, Guid userId);
        Task DeleteCategoryAsync(Guid id, Guid userId);
    }
}

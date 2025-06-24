using AdvancedTodoApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedTodoApp.Application.DTOs.Category;
using AdvancedTodoApp.Application.Common;

namespace AdvancedTodoApp.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<OperationResult<CategoryDto>> GetByIdAsync(Guid id, Guid userId);
        Task<OperationResult<List<CategoryDto>>> GetAllByUserIdAsync(Guid userId);
        Task<OperationResult<CategoryDto>> AddCategoryAsync(CreateCategoryDto dto, Guid userId);
        Task<OperationResult<string>> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDto dto, Guid userId);
        Task<OperationResult<string>> DeleteCategoryAsync(Guid id, Guid userId);
    }
}

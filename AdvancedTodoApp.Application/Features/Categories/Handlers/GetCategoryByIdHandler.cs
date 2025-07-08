using AdvancedTodoApp.Application.Common;
using AdvancedTodoApp.Application.DTOs.Category;
using AdvancedTodoApp.Application.Features.Categories.Queries;
using AdvancedTodoApp.Application.Interfaces.Persistence;
using AdvancedTodoApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.Features.Categories.Handlers
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, OperationResult<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryByIdHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<OperationResult<CategoryDto>> Handle (GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(query.CategoryId);
            if (category == null || category.UserId != query.UserId)
            {
                return OperationResult<CategoryDto>.Fail(
                    "Category not found or unauthorized.",
                    ErrorCode.NotFound
                );
            }

            var dto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
            };

            return OperationResult<CategoryDto>.Ok(dto);

        }
    }
}

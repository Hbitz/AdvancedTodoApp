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
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, OperationResult<List<CategoryDto>>>
    {
        public readonly ICategoryRepository _categoryRepository;

        public GetCategoriesHandler(ICategoryRepository categoryRepository) {
            _categoryRepository = categoryRepository;
        }

        public async Task<OperationResult<List<CategoryDto>>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
        { 
            var categories = await _categoryRepository.GetAllByUserIdAsync(query.UserId);
            var dtos = categories
                .Select(c => new CategoryDto { Id = c.Id, Name = c.Name })
                .ToList();

            return OperationResult<List<CategoryDto>>.Ok(dtos);
        }
    }
}

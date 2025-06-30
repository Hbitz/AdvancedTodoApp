using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedTodoApp.Application.Common;
using AdvancedTodoApp.Application.DTOs;
using AdvancedTodoApp.Application.Features.Categories.Commands;
using AdvancedTodoApp.Application.Interfaces;
using AdvancedTodoApp.Domain.Entities;
using MediatR;
using System.Net;
using AdvancedTodoApp.Application.DTOs.Category;
using AdvancedTodoApp.Application.Interfaces.Persistence;

namespace AdvancedTodoApp.Application.Features.Categories.Handlers
{
    // This handler listens for CreateCategoryCommand request from MediatR and returns OperationResult<CategoryDto> after handling them.
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, OperationResult<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryHandler(ICategoryRepository categorRepository) 
        {
            _categoryRepository = categorRepository;
        }

        // Most of this logic is just a copy of what the original CategoryService Add-method would have, in case we didin't user MediatR.
        public async Task<OperationResult<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            // Map command to entity
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                UserId = request.UserId,
            };

            _categoryRepository.Add(category);
            await _categoryRepository.SaveChangesAsync();

            // Create dto
            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
            };

            return OperationResult<CategoryDto>.Ok(categoryDto, HttpStatusCode.Created);
        }
    }
}

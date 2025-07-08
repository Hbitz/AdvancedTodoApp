using AdvancedTodoApp.Application.Common;
using AdvancedTodoApp.Application.DTOs.Category;
using AdvancedTodoApp.Application.Features.Categories.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedTodoApp.Domain.Entities;
using MediatR;
using System.Net;
using AdvancedTodoApp.Application.DTOs.Category;
using AdvancedTodoApp.Application.Interfaces.Persistence;

namespace AdvancedTodoApp.Application.Features.Categories.Handlers
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, OperationResult<string>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<OperationResult<string>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var existing = await _categoryRepository.GetByIdAsync(command.CategoryId);
            if (existing == null)
            {
                return OperationResult<string>.Fail("Category not found.", ErrorCode.NotFound);
            }

            if (existing.UserId != command.UserId)
            {
                return OperationResult<string>.Fail("Unauthorized to update this category.", ErrorCode.Unauthorized);
            }


            existing.Name = command.Name;
            _categoryRepository.Update(existing);
            await _categoryRepository.SaveChangesAsync();

            return OperationResult<string>.Ok("Category updated successfully.");

        }
    }
}

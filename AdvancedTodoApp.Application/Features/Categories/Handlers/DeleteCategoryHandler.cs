using AdvancedTodoApp.Application.Common;
using AdvancedTodoApp.Application.Features.Categories.Commands;
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
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, OperationResult<string>>
    {
        private readonly ICategoryRepository _categoryRepository;
    
        public DeleteCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<OperationResult<string>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            var existing = await _categoryRepository.GetByIdAsync(command.CategoryId);
            if (existing == null)
            {
                return OperationResult<string>.Fail("Category not found.");
            }

            if (existing.UserId != command.UserId)
            {
                return OperationResult<string>.Fail("Unauthorized to delete this category.", ErrorCode.Unauthorized);
            }

            _categoryRepository.Delete(existing);
            await _categoryRepository.SaveChangesAsync();

            return OperationResult<string>.Ok("Category deleted successfully");

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedTodoApp.Application.Common;
using AdvancedTodoApp.Application.DTOs.Category;
using MediatR;

namespace AdvancedTodoApp.Application.Features.Categories.Commands
{
    // This defines the "request" for mediatR
    public class CreateCategoryCommand : IRequest<OperationResult<CategoryDto>>
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }

        // Manual validation and logic could be implemented here, but FluentValidation could be implemented instead
    }
}

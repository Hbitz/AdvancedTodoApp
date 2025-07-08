using AdvancedTodoApp.Application.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedTodoApp.Application.Common;
using MediatR;

namespace AdvancedTodoApp.Application.Features.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<OperationResult<string>>
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedTodoApp.Application.Common;
using AdvancedTodoApp.Application.DTOs.Category;
using MediatR;

namespace AdvancedTodoApp.Application.Features.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<OperationResult<CategoryDto>>
    {
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
    }
}

using AdvancedTodoApp.Application.Common;
using AdvancedTodoApp.Application.DTOs.Category;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.Features.Categories.Queries
{
    public class GetCategoriesQuery : IRequest<OperationResult<List<CategoryDto>>>
    {
        public Guid UserId { get; set; }
    }
}

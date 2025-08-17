using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using AdvancedTodoApp.Application.DTOs.Todo;
using AdvancedTodoApp.Application.Common.Models;
using AdvancedTodoApp.Application.Common;

namespace AdvancedTodoApp.Application.Features.Categories.Queries
{
    public class GetTodoByIdQuery : IRequest<OperationResult<TodoDto>>
    {
        public Guid TodoId { get; set; }
        public Guid UserId { get; set; }
    }
}

using AdvancedTodoApp.Application.Common;
using AdvancedTodoApp.Application.DTOs.Todo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.Features.Todos.Queries
{
    public class GetTodosQuery : IRequest<OperationResult<List<TodoDto>>>
    {
        public Guid UserId { get; set; }
    }
}

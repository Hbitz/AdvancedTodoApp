using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using AdvancedTodoApp.Application.DTOs.Todo;
using AdvancedTodoApp.Application.Common.Models;
using AdvancedTodoApp.Application.Common;

namespace AdvancedTodoApp.Application.Features.Todos.Commands
{

    // This should be what the handler returns, e.g. a TodoDto and not a UpdatedTodoDto
    // Reminder: UpdateTodoDto typically represents the *input* from user when updating, while TodoDto represents the *read model*
    public class UpdateTodoCommand : IRequest<OperationResult<TodoDto>>
    {
        public Guid TodoId { get; set; }
        public UpdateTodoDto UpdateTodoDto { get; set; }
        public Guid UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using AdvancedTodoApp.Application.Common.Models;
using AdvancedTodoApp.Application.Common;

namespace AdvancedTodoApp.Application.Features.Todos.Commands
{
    public class DeleteTodoCommand : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }
    }
}

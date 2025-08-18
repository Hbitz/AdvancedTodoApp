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
    public class CreateTodoCommand : IRequest<OperationResult<TodoDto>>
    {
        // Wrapping in DTO this time instead of typing individual properties as i did with Category commands
        // Pros: Reuses DTO class in multiple places, keeps commands focused on behavior and DTOs on data structure
        // Cons: A bit of extra nesting, and requires another layer when accessing command: command.Title vs command.CreateTodoDto.Title.
        public CreateTodoDto CreateTodoDto { get; set; }
        public Guid UserId { get; set; }
    }
}

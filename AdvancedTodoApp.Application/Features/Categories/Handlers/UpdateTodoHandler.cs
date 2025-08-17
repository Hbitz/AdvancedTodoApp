using AdvancedTodoApp.Application.Common;
using AdvancedTodoApp.Application.DTOs.Todo;
using AdvancedTodoApp.Application.Features.Categories.Commands;
using AdvancedTodoApp.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.Features.Categories.Handlers
{
    public class UpdateTodoHandler : IRequestHandler<UpdateTodoCommand, OperationResult<TodoDto>>
    {
        private readonly ITodoRepository _todoRepository;

        public UpdateTodoHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<OperationResult<TodoDto>> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(request.TodoId);
            if (todo == null)
            {
                // TODO: fix explicit typed statusCode argument
                return OperationResult<TodoDto>.Fail("Todo not found", statusCode: HttpStatusCode.NotFound);                
            }

            // Update properties.
            todo.Title = request.UpdateTodoDto.Title;
            todo.Description = request.UpdateTodoDto.Description;   
            todo.IsCompleted = request.UpdateTodoDto.IsCompleted;
            todo.UserId = request.UserId;
            todo.CategoryId = request.UpdateTodoDto.CategoryId;

            _todoRepository.Update(todo);
            await _todoRepository.SaveChangesAsync();

            // Map to DTO to return
            var todoDto = new TodoDto
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted,
                UserId = todo.UserId,
                CategoryId = todo.CategoryId,
            };

            return OperationResult<TodoDto>.Ok(todoDto, HttpStatusCode.OK);
        }
    }
}

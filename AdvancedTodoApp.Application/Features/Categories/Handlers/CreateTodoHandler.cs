using AdvancedTodoApp.Application.Common;
using AdvancedTodoApp.Application.DTOs.Todo;
using AdvancedTodoApp.Application.Features.Categories.Commands;
using AdvancedTodoApp.Application.Interfaces.Persistence;
using AdvancedTodoApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.Features.Categories.Handlers
{
    public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, OperationResult<TodoDto>>
    {
        private readonly ITodoRepository _todoRepository;

        public CreateTodoHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<OperationResult<TodoDto>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            //Map dto to entity
            var todo = new TodoItem
            {
                Id = Guid.NewGuid(),
                Title = request.CreateTodoDto.Title,
                Description = request.CreateTodoDto.Description,
                IsCompleted = request.CreateTodoDto.isCompleted,
                UserId = request.CreateTodoDto.UserId,
                CategoryId = request.CreateTodoDto.CategoryId,
            };

            _todoRepository.Add(todo);
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

            return OperationResult<TodoDto>.Ok(todoDto, HttpStatusCode.Created);
        }
    }
}

using AdvancedTodoApp.Application.Common;
using AdvancedTodoApp.Application.DTOs.Todo;
using AdvancedTodoApp.Application.Interfaces.Persistence;
using AdvancedTodoApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using AdvancedTodoApp.Application.Features.Todos.Commands;

namespace AdvancedTodoApp.Application.Features.Todos.Handlers
{
    public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, OperationResult<TodoDto>>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CreateTodoHandler(ITodoRepository todoRepository, ICategoryRepository categoryRepository)
        {
            _todoRepository = todoRepository;
            _categoryRepository = categoryRepository;
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
                UserId = request.UserId,
                CategoryId = request.CreateTodoDto.CategoryId,
            }; 
            
            // categoryId is optional, but if value exists, validate it.
            if (request.CreateTodoDto.CategoryId.HasValue)
            {
                var category = await _todoRepository.GetByIdAsync(request.CreateTodoDto.CategoryId.Value);
                if (category == null)
                {
                    return OperationResult<TodoDto>.Fail("Category not found", ErrorCode.NotFound, HttpStatusCode.BadRequest);
                }
            }

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

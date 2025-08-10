using AdvancedTodoApp.Application.Common;
using AdvancedTodoApp.Application.DTOs.Todo;
using AdvancedTodoApp.Application.Features.Categories.Queries;
using AdvancedTodoApp.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.Features.Categories.Handlers
{
    public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, OperationResult<List<TodoDto>>>
    {
        private readonly ITodoRepository _todoRepository;

        public GetTodosQueryHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<OperationResult<List<TodoDto>>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            var todos = await _todoRepository.GetAllByUserIdAsync(request.UserId);

            var todoDtos = todos.Select(todos => new TodoDto
            {
                Id = todos.Id,
                Title = todos.Title,
                Description = todos.Description,
                IsCompleted = todos.IsCompleted,
                UserId = todos.UserId,
                CategoryId = todos.CategoryId,
            }).ToList();

            return OperationResult<List<TodoDto>>.Ok(todoDtos);
        }
    }
}

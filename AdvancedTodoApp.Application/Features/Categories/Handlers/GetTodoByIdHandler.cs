using AdvancedTodoApp.Application.Common;
using AdvancedTodoApp.Application.DTOs.Todo;
using AdvancedTodoApp.Application.Features.Categories.Queries;
using AdvancedTodoApp.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.Features.Categories.Handlers
{
    public class GetTodoByIdHandler : IRequestHandler<GetTodoByIdQuery, OperationResult<TodoDto>>
    {
        private readonly ITodoRepository _todoRepository;

        public GetTodoByIdHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<OperationResult<TodoDto>> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(request.TodoId);

            if (todo == null || todo.UserId != request.UserId)
            {
                return OperationResult<TodoDto>.Fail("Todo not found or unauthorized.", statusCode: HttpStatusCode.NotFound);
            }

            var dto = new TodoDto
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted,
                UserId = request.UserId,
                CategoryId = todo.CategoryId
            };

            return OperationResult<TodoDto>.Ok(dto);
        }
    }
}

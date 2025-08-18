using AdvancedTodoApp.Application.Common;
using AdvancedTodoApp.Application.Interfaces.Persistence;
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
    public class DeleteTodoHandler : IRequestHandler<DeleteTodoCommand, OperationResult<bool>>
    {
        private readonly ITodoRepository _todoRepository;

        public DeleteTodoHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<OperationResult<bool>> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(request.Id);
            if (todo == null)
            {
                return OperationResult<bool>.Fail("Todo not found", statusCode: HttpStatusCode.NotFound);
            }

            _todoRepository.Delete(todo);
            await _todoRepository.SaveChangesAsync();

            return OperationResult<bool>.Ok(true, HttpStatusCode.NoContent);
        }
    }
}

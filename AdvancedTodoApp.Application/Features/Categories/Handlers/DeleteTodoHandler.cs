using AdvancedTodoApp.Application.Common;
using AdvancedTodoApp.Application.Features.Categories.Commands;
using AdvancedTodoApp.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.Features.Categories.Handlers
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
            var todo = await _todoRepository.GetByIdAsync(request.id);
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

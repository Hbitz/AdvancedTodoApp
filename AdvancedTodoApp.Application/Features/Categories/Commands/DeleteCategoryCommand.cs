using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedTodoApp.Application.Common;
using MediatR;

namespace AdvancedTodoApp.Application.Features.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<OperationResult<string>>
    {
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.Features.Todos.Queries
{
    public class GetTodoByIdQueryValidator : AbstractValidator<GetTodoByIdQuery>
    {
        public GetTodoByIdQueryValidator() 
        {
            RuleFor(x => x.TodoId)
                .NotEmpty();

            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}

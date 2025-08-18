using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.Features.Todos.Queries
{
    public class GetTodosQueryValidator : AbstractValidator<GetTodosQuery>
    {
        public GetTodosQueryValidator() 
        {
            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.Features.Categories.Queries
{
    public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdQueryValidator() 
        {
            RuleFor(x => x.UserId).
                NotEmpty();

            RuleFor(x => x.CategoryId)
                .NotEmpty();
        }
    }
}

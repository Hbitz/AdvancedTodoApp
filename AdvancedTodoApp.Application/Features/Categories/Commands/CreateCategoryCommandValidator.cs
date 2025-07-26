using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AdvancedTodoApp.Application.Features.Categories.Commands
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Name cannot be just whitespace.")
                .MaximumLength(100).WithMessage("Catgory name must be less than 100 characters.");
        }
    }
}

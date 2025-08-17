using AdvancedTodoApp.Application.DTOs.Todo;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.Features.Categories.Commands
{
    public class CreateTodoValidator : AbstractValidator<CreateTodoDto>
    {
        public CreateTodoValidator() 
        {
            RuleFor(x => x.Title)
                .NotNull().WithMessage("Title is required")
                .NotEmpty().WithMessage("Title name is required.")
                .Must(title => !string.IsNullOrWhiteSpace(title)).WithMessage("Title cannot be just whitespace.")
                .Length(1, 100).WithMessage("Title name must be less than 100 characters.");
        }
    }
}

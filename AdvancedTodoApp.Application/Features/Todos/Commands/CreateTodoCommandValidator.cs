using AdvancedTodoApp.Application.DTOs.Todo;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.Features.Todos.Commands
{
    public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
    {
        public CreateTodoCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.CreateTodoDto.Title)
                .NotNull().WithMessage("Title is required")
                .NotEmpty().WithMessage("Title name is required.")
                .Must(title => !string.IsNullOrWhiteSpace(title)).WithMessage("Title cannot be just whitespace.")
                .Length(1, 100).WithMessage("Title name must be less than 100 characters.");

            RuleFor(x => x.CreateTodoDto.Description)
                .MaximumLength(200).WithMessage("Description cannot exceed 200 characters");
        }
    }
}

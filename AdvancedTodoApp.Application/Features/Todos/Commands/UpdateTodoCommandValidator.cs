using AdvancedTodoApp.Application.DTOs.Todo;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.Features.Todos.Commands
{
    public class UpdateTodoCommandValidator : AbstractValidator<UpdateTodoCommand>
    {
        public UpdateTodoCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.UpdateTodoDto.Description)
                .MaximumLength(200);

            RuleFor(x => x.UpdateTodoDto.Title)
                .NotEmpty().WithMessage("Todo title is required.")
                .Must(title => !string.IsNullOrWhiteSpace(title)).WithMessage("Title cannot be just whitespace.")
                .Length(1, 100).WithMessage("Todo title  must be less than 100 characters.");
        }
    }
}

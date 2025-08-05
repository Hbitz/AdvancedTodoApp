using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AdvancedTodoApp.Application.Features.Categories.Commands
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator() {

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required")
                .Must(username => !string.IsNullOrWhiteSpace(username)).WithMessage("Username cannot be just whitespace.")
                .MaximumLength(50).WithMessage("Username must be less than 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .Must(email => !string.IsNullOrWhiteSpace(email)).WithMessage("Email cannot be just whitespace.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .Must(password => !string.IsNullOrWhiteSpace(password)).WithMessage("Password cannot be just whitespace.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
        }
    }
}

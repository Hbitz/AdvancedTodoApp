using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.Features.Auth.Commands
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator() 
        {
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

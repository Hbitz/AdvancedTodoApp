using AdvancedTodoApp.Application.Common;
using AdvancedTodoApp.Application.DTOs.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedTodoApp.Application.Features.Categories.Commands
{
    public class LoginCommand : IRequest<OperationResult<JwtTokenDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}

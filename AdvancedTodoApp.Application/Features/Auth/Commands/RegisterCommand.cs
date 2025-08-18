using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedTodoApp.Application.DTOs.Auth;
using AdvancedTodoApp.Application.Common;
using MediatR;

namespace AdvancedTodoApp.Application.Features.Auth.Commands
{
    public class RegisterCommand : IRequest<OperationResult<RegisteredUserDto>>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

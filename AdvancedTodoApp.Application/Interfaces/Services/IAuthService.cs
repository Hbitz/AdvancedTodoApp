using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedTodoApp.Application.DTOs.Auth;
using AdvancedTodoApp.Application.Common;

namespace AdvancedTodoApp.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<OperationResult<string>> RegisterAsync(RegisterUserDto dto);
        Task<OperationResult<JwtTokenDto>> LoginAsync(LoginUserDto dto);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedTodoApp.Application.DTOs.Auth;
using AdvancedTodoApp.Domain.Entities;

namespace AdvancedTodoApp.Application.Interfaces.Auth
{
    // We refactor Auth to go from using an AuthService to full MediatR.
    // Since application layer should be focused on purely business logic and not dependant on framework-specifc packages, we just create a interface here and implement it in Infrastructure.
    public interface IJwtTokenGenerator
    {
        JwtTokenDto GenerateToken(User user);
    }
}

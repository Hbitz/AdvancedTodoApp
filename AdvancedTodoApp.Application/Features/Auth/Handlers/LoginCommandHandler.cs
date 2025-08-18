using AdvancedTodoApp.Application.Common;
using AdvancedTodoApp.Application.DTOs.Auth;
using AdvancedTodoApp.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Cryptography;
using AdvancedTodoApp.Application.Interfaces.Auth;
using System.Net;
using AdvancedTodoApp.Application.Features.Auth.Commands;

namespace AdvancedTodoApp.Application.Features.Auth.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult<JwtTokenDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<OperationResult<JwtTokenDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null || user.PasswordHash != HashPassword(request.Password))
            {
                return OperationResult<JwtTokenDto>.Fail("Invalid email or password.", statusCode: HttpStatusCode.Unauthorized);
            }

            var token = _jwtTokenGenerator.GenerateToken(user);
            return OperationResult<JwtTokenDto>.Ok(token);
        }

        public string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

    }
}

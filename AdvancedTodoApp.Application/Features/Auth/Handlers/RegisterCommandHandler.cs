using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedTodoApp.Application.Common;
using AdvancedTodoApp.Application.DTOs.Auth;
using AdvancedTodoApp.Application.Interfaces.Persistence;
using AdvancedTodoApp.Domain.Entities;
using MediatR;
using System.Security.Cryptography;
using System.Text;
using AdvancedTodoApp.Application.Features.Auth.Commands;

namespace AdvancedTodoApp.Application.Features.Auth.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, OperationResult<RegisteredUserDto>>
    {
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult<RegisteredUserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Check if email exists
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return OperationResult<RegisteredUserDto>.Fail("User already exists.", ErrorCode.Conflict);
            }

            // Else, create new user
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                UserName = request.UserName,
                PasswordHash = HashPassword(request.Password),
            };

            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();

            var dto = new RegisteredUserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName
            };

            return OperationResult<RegisteredUserDto>.Ok(dto);
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}

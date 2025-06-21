using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AdvancedTodoApp.Application.Common;
using AdvancedTodoApp.Application.DTOs.Auth;
using AdvancedTodoApp.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using AdvancedTodoApp.Application.Interfaces.Persistence;
using AdvancedTodoApp.Domain.Entities;
using System.Security.Cryptography;

namespace AdvancedTodoApp.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<OperationResult<string>> RegisterAsync(RegisterUserDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                return OperationResult<string>.Fail("User already exists");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                UserName = dto.UserName
            };

            user.PasswordHash = HashPassword(dto.Password);

            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();

            return OperationResult<string>.Ok("User registered successfully.");
        }

        public async Task<OperationResult<string>> LoginAsync(LoginUserDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || user.PasswordHash != HashPassword(dto.Password))
            {
                return OperationResult<string>.Fail("Invalid email or password.");
            }

            var token = GenerateJwtToken(user);
            return OperationResult<string>.Ok(token);
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

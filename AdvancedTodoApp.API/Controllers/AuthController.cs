using AdvancedTodoApp.Application.DTOs.Auth;
using AdvancedTodoApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedTodoApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            return FromResult(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            return FromResult(result);
        }
    }
}

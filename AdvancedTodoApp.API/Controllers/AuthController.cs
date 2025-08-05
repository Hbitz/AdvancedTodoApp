using AdvancedTodoApp.Application.DTOs.Auth;
using AdvancedTodoApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AdvancedTodoApp.Application.Features.Categories.Commands;

namespace AdvancedTodoApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseApiController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            var command = new RegisterCommand
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Password = dto.Password
            };
            var result = await _mediator.Send(command); 
            return FromResult(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            var command = new LoginCommand
            {
                Email = dto.Email,
                Password = dto.Password
            };
            var result = await _mediator.Send(command);
            return FromResult(result);
        }
    }
}

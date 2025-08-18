using AdvancedTodoApp.Application.DTOs.Auth;
using AdvancedTodoApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AdvancedTodoApp.Application.Features.Auth.Commands;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "Register a new user", Description = "Creates a new user with username, email, and password.")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [SwaggerOperation(Summary = "Login a user", Description = "Returns JWT token on successful login.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

using Lamba.Identity.Application.Features.Commands.Authentications;
using Lamba.Identity.Application.Features.Commands.Authentications.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lamba.Identity.Api.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<RegisterResponseDto> Register(RegisterRequestDto register, CancellationToken cancellationToken)
        {
            var registerCommand = new RegisterCommand
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                Username = register.Username,
                Password = register.Password
            };
            return await _mediator.Send(registerCommand, cancellationToken);
        }

        [HttpPost("Login")]
        public async Task<LoginResponseDto> Login(LoginRequestDto login, CancellationToken cancellationToken)
        {
            var loginCommand = new LoginCommand
            {
                Username = login.Username,
                Password = login.Password
            };
            return await _mediator.Send(loginCommand, cancellationToken);
        }
    }
}

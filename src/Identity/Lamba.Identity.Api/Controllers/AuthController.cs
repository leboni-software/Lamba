using Lamba.Identity.Application.Features.Commands.Authentications;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        public async Task<IActionResult> Register(RegisterCommand register, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(register, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginCommand login, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(login, cancellationToken);
            return Ok(result);
        }
    }
}

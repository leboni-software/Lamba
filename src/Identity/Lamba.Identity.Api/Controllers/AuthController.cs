using Lamba.Common.Models.Results;
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

        [HttpPost]
        public async Task<RegisterResponseDto> Register(RegisterCommand register, CancellationToken cancellationToken)
        {
            return await _mediator.Send(register, cancellationToken);
        }

        [HttpPost]
        public async Task<LoginResponseDto> Login(LoginCommand login, CancellationToken cancellationToken)
        {
            return await _mediator.Send(login, cancellationToken);
        }
    }
}

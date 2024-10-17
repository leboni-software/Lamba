using Lamba.Identity.Application.Features.Commands.Authentications;
using Lamba.Identity.Application.Features.Queries.Users;
using Lamba.Security.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lamba.Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new RegisterCommand
            {
                FirstName = "Test",
                LastName = "Test",
                Username = Guid.NewGuid().ToString(),
                Email = "Test",
                Password = "Test"
            }, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Users(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUserQuery(), cancellationToken);
            return Ok(result);
        }
    }
}

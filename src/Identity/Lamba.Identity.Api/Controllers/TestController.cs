using Lamba.Identity.Application.Features.Commands.Authentications;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public async Task<IActionResult> Test(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new RegisterCommand
            {
                FirstName = "Test",
                LastName = "Test",
                Username = "Test",
                Email = "Test",
                Password = "Test"
            }, cancellationToken);
            return Ok(result);
        }
    }
}

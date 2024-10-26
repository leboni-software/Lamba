using Lamba.Identity.Application.Features.Commands.Roles;
using Lamba.Identity.Application.Features.Commands.Roles.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lamba.Identity.Api.Controllers
{
    [Authorize]
    public class RolesController : BaseApiController
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<Guid> Create(CreateRoleRequestDto requestDto, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new CreateRoleCommand
            {
                Name = requestDto.Name
            }, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<bool> Update([FromRoute] Guid id, UpdateRoleRequestDto requestDto, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new UpdateRoleCommand
            {
                Id = id,
                Name = requestDto.Name
            }, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new DeleteRoleCommand
            {
                Id = id
            }, cancellationToken);
        }
    }
}

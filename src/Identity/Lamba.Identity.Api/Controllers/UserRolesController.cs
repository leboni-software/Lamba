using Lamba.Identity.Application.Features.Commands.UserRoles;
using Lamba.Identity.Application.Features.Commands.UserRoles.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lamba.Identity.Api.Controllers
{
    public class UserRolesController : BaseApiController
    {
        private readonly IMediator _mediator;

        public UserRolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<Guid> Create(AddUserRoleRequestDto requestDto, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new AddUserRoleCommand
            {
                UserId = requestDto.UserId,
                RoleId = requestDto.RoleId
            }, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new DeleteUserRoleCommand { Id = id }, cancellationToken);
        }
    }
}

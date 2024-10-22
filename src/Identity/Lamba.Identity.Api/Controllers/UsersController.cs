using Lamba.Identity.Application.Features.Commands.Users;
using Lamba.Identity.Application.Features.Commands.Users.Dto;
using Lamba.Identity.Application.Features.Queries.Users;
using Lamba.Identity.Application.Features.Queries.Users.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lamba.Identity.Api.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{id}")]
        public async Task<bool> Update([FromRoute] Guid id, UpdateUserRequestDto updateUser, CancellationToken cancellationToken)
        {
            var updateCommand = new UpdateUserCommand
            {
                FirstName = updateUser.FirstName,
                LastName = updateUser.LastName,
                Email = updateUser.Email,
                Username = updateUser.Username,
                Id = id
            };
            return await _mediator.Send(updateCommand, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<UserResponseDto> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetUserQuery { Id = id }, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new DeleteUserCommand { Id = id }, cancellationToken);
        }
    }
}

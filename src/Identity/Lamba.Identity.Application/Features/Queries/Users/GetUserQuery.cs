using Lamba.Identity.Application.Common.Handlers;
using Lamba.Identity.Application.Features.Queries.Users.Dto;
using Lamba.Identity.Application.Infrastructure.Repositories.Readers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lamba.Identity.Application.Features.Queries.Users
{
    public class GetUserQuery : BaseAuthorizeRequest<UserResponseDto>
    {
    }

    public class GetUserQeuryHandler : IRequestHandler<GetUserQuery, UserResponseDto>
    {
        private readonly IUserReaderRepository _userReaderRepository;

        public GetUserQeuryHandler(IUserReaderRepository userReaderRepository)
        {
            _userReaderRepository = userReaderRepository;
        }

        public async Task<UserResponseDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _userReaderRepository.GetQueryable()
                .Select(x => new UserResponseDto
                {
                    Username = x.Username,
                    Email = x.Email,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                })
                .FirstAsync(cancellationToken);
        }
    }
}

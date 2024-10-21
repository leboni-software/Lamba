using Lamba.Identity.Application.Common.Constants;
using Lamba.Identity.Application.Common.Handlers;
using Lamba.Identity.Application.Features.Queries.Users.Dto;
using Lamba.Identity.Application.Infrastructure.Repositories.Readers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lamba.Identity.Application.Features.Queries.Users
{
    public class GetUserQuery : BaseAuthorizeRequest<UserResponseDto>
    {
        public required Guid Id { get; set; }
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
            var user = await _userReaderRepository.GetQueryable()
                .Where(x => x.Id == request.Id)
                .Select(x => new UserResponseDto
                {
                    Username = x.Username,
                    Email = x.Email,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    Id = x.Id
                })
                .FirstOrDefaultAsync(cancellationToken);
            if (user is null) throw new Exception(UserMessages.UserNotFound);
            return user;
        }
    }
}

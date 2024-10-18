using Lamba.Identity.Application.Features.Commands.Authentications.Dto;
using Lamba.Identity.Application.Infrastructure.Repositories.Readers;
using Lamba.Security.Abstract;
using Lamba.Security.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lamba.Identity.Application.Features.Commands.Authentications
{
    public record class LoginCommand : IRequest<LoginResponseDto>
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IUserReaderRepository _userReaderRepository;

        public LoginCommandHandler(ITokenProvider tokenProvider, IUserReaderRepository userReaderRepository)
        {
            _tokenProvider = tokenProvider;
            _userReaderRepository = userReaderRepository;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userReaderRepository.GetQueryable()
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .Where(x => x.Username == request.Username)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new Exception("Incorrect username!");
            if (user.Password != HashHelper.ComputeHash(request.Password, user.PasswordSalt))
                throw new Exception("Incorrect password!");
            var token = _tokenProvider.CreateToken(user.Username, string.Join(",", user.UserRoles.Select(x => x.Role.Name)));
            return new LoginResponseDto { Token = token };
        }
    }
}

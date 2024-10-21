using Lamba.Identity.Application.Common.Constants;
using Lamba.Identity.Application.Features.Commands.Authentications.Dto;
using Lamba.Identity.Application.Infrastructure.Repositories.Readers;
using Lamba.Identity.Application.Infrastructure.Repositories.UoW;
using Lamba.Identity.Application.Infrastructure.Repositories.Writers;
using Lamba.Identity.Domain.Entities;
using Lamba.Security.Abstract;
using Lamba.Security.Common;
using MediatR;

namespace Lamba.Identity.Application.Features.Commands.Authentications
{
    public record RegisterCommand : IRequest<RegisterResponseDto>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponseDto>
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IUserWriterRepository _userWriterRepository;
        private readonly IRoleReaderRepository _roleReaderRepository;
        private readonly IIdentityUnitOfWork _identityUnitOfWork;

        public RegisterCommandHandler(ITokenProvider tokenProvider, IUserWriterRepository userWriterRepository, IRoleReaderRepository roleReaderRepository, IIdentityUnitOfWork identityUnitOfWork)
        {
            _tokenProvider = tokenProvider;
            _userWriterRepository = userWriterRepository;
            _roleReaderRepository = roleReaderRepository;
            _identityUnitOfWork = identityUnitOfWork;
        }

        public async Task<RegisterResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new User(
                request.FirstName,
                request.LastName,
                request.Username,
                request.Email,
                request.Password
            );
            var defaultRole = await _roleReaderRepository.GetAsync(x => x.IsDefaultRole, cancellationToken);
            if (defaultRole is null) throw new Exception(AuthenticationMessages.DefaultRoleNotFound);
            await _identityUnitOfWork.ExecuteTransactionAsync(async () =>
            {
                user.UserRoles.Add(new UserRole { User = user, RoleId = defaultRole.Id });
                await _userWriterRepository.AddAsync(user, cancellationToken);
            }, cancellationToken);
            var token = _tokenProvider.CreateToken(user.Id, user.Username, defaultRole.Name);
            return new RegisterResponseDto { Token = token };
        }
    }
}

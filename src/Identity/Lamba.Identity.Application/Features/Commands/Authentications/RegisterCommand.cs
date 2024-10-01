using Lamba.Identity.Application.Features.Commands.Authentications.Dto;
using Lamba.Identity.Application.Infrastructure.Repositories.Readers;
using Lamba.Identity.Application.Infrastructure.Repositories.UoW;
using Lamba.Identity.Application.Infrastructure.Repositories.Writers;
using Lamba.Identity.Domain.Entities;
using Lamba.Security.Abstract;
using Lamba.Security.Common;
using MediatR;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace Lamba.Identity.Application.Features.Commands.Authentications
{
    public record RegisterCommand : IRequest<RegisterReponseDto>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterReponseDto>
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

        public async Task<RegisterReponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var passwordSalt = HashHelper.GenerateSalt();
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Email = request.Email,
                PasswordSalt = passwordSalt,
                Password = HashHelper.ComputeHash(request.Password, passwordSalt)
            };
            var defaultRole = await _roleReaderRepository.GetAsync(x => x.IsDefaultRole, cancellationToken);
            if (defaultRole is null) throw new Exception("The default role was not found!");
            await _identityUnitOfWork.ExecuteTransactionAsync(async () =>
            {
                user.UserRoles.Add(new UserRole { User = user, RoleId = defaultRole.Id });
                await _userWriterRepository.AddAsync(user, cancellationToken);
            }, cancellationToken);
            var token = _tokenProvider.CreateToken(user.Username, defaultRole.Name);
            return new RegisterReponseDto { Token = token };
        }
    }
}

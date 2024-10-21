using Lamba.Identity.Application.Common.Constants;
using Lamba.Identity.Application.Common.Handlers;
using Lamba.Identity.Application.Infrastructure.Repositories.Readers;
using Lamba.Identity.Application.Infrastructure.Repositories.Writers;
using MediatR;

namespace Lamba.Identity.Application.Features.Commands.Users
{
    public class UpdateUserCommand : BaseAuthorizeRequest<bool>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserReaderRepository _userReaderRepository;
        private readonly IUserWriterRepository _userWriterRepository;

        public UpdateUserCommandHandler(IUserWriterRepository userWriterRepository, IUserReaderRepository userReaderRepository)
        {
            _userWriterRepository = userWriterRepository;
            _userReaderRepository = userReaderRepository;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userReaderRepository.GetAsync(request.Id, cancellationToken);
            if (user is null) throw new Exception(UserMessages.UserNotFound);
            user.SetFirstName(request.FirstName);
            user.SetLastName(request.LastName);
            user.SetUsername(request.Username);
            user.SetEmail(request.Email);
            _userWriterRepository.Attach(user);
            _userWriterRepository.Update(user);
            await _userWriterRepository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

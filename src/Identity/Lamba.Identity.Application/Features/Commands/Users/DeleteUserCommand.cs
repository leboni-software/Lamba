using Lamba.Identity.Application.Common.Accessors;
using Lamba.Identity.Application.Common.Constants;
using Lamba.Identity.Application.Common.Handlers;
using Lamba.Identity.Application.Infrastructure.Repositories.Readers;
using Lamba.Identity.Application.Infrastructure.Repositories.Writers;
using MediatR;

namespace Lamba.Identity.Application.Features.Commands.Users
{
    public class DeleteUserCommand : BaseAuthorizeRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserWriterRepository _userWriterRepository;
        private readonly IUserReaderRepository _userReaderRepository;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public DeleteUserCommandHandler(IUserWriterRepository userWriterRepository, IUserReaderRepository userReaderRepository, ICurrentUserAccessor currentUserAccessor)
        {
            _userWriterRepository = userWriterRepository;
            _userReaderRepository = userReaderRepository;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userReaderRepository.GetAsync(request.Id, cancellationToken);
            if (user is null) throw new Exception(UserMessages.UserNotFound);
            user.DeletedUserId = _currentUserAccessor.GetId();
            _userWriterRepository.Attach(user);
            _userWriterRepository.Delete(user);
            await _userWriterRepository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

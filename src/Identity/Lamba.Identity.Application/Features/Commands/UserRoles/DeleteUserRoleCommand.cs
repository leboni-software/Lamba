using Lamba.Identity.Application.Common.Accessors;
using Lamba.Identity.Application.Common.Constants;
using Lamba.Identity.Application.Common.Handlers;
using Lamba.Identity.Application.Infrastructure.Repositories.Readers;
using Lamba.Identity.Application.Infrastructure.Repositories.Writers;

namespace Lamba.Identity.Application.Features.Commands.UserRoles
{
    public class DeleteUserRoleCommand : BaseAuthorizeRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteUserRoleCommandHandler : BaseAuthorizeRequestHandler<DeleteUserRoleCommand, bool>
    {
        private readonly IUserRoleWriterRepository _userRoleWriterRepository;
        private readonly IUserRoleReaderRepository _userRoleReaderRepository;

        public DeleteUserRoleCommandHandler(ICurrentUserAccessor? currentUserAccessor, IUserRoleWriterRepository userRoleWriterRepository, IUserRoleReaderRepository userRoleReaderRepository) : base(currentUserAccessor)
        {
            _userRoleWriterRepository = userRoleWriterRepository;
            _userRoleReaderRepository = userRoleReaderRepository;
        }

        public override async Task<bool> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
        {
            var userRole = await _userRoleReaderRepository.GetAsync(request.Id, cancellationToken);
            if (userRole is null) throw new Exception(UserRoleMessages.UserRoleNotFound);
            userRole.DeletedUserId = _currentUserAccessor?.GetId();
            _userRoleWriterRepository.Attach(userRole);
            _userRoleWriterRepository.Delete(userRole);
            await _userRoleWriterRepository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

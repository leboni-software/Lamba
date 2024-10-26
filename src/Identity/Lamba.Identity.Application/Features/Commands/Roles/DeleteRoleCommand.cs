using Lamba.Identity.Application.Common.Accessors;
using Lamba.Identity.Application.Common.Constants;
using Lamba.Identity.Application.Common.Handlers;
using Lamba.Identity.Application.Infrastructure.Repositories.Readers;
using Lamba.Identity.Application.Infrastructure.Repositories.Writers;

namespace Lamba.Identity.Application.Features.Commands.Roles
{
    public class DeleteRoleCommand : BaseAuthorizeRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteRoleCommandHandler : BaseAuthorizeRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IRoleWriterRepository _roleWriterRepository;
        private readonly IRoleReaderRepository _roleReaderRepository;

        public DeleteRoleCommandHandler(ICurrentUserAccessor currentUserAccessor, IRoleWriterRepository roleWriterRepository, IRoleReaderRepository roleReaderRepository) : base(currentUserAccessor)
        {
            _roleWriterRepository = roleWriterRepository;
            _roleReaderRepository = roleReaderRepository;
        }

        public override async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleReaderRepository.GetAsync(request.Id, cancellationToken);
            if (role is null) throw new Exception(RoleMessages.RoleNotFound);
            role.DeletedUserId = _currentUserAccessor?.GetId();
            _roleWriterRepository.Attach(role);
            _roleWriterRepository.Delete(role);
            await _roleWriterRepository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

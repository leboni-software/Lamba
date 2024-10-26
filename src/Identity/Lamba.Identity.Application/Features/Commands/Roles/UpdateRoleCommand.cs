using Lamba.Identity.Application.Common.Accessors;
using Lamba.Identity.Application.Common.Constants;
using Lamba.Identity.Application.Common.Handlers;
using Lamba.Identity.Application.Infrastructure.Repositories.Readers;
using Lamba.Identity.Application.Infrastructure.Repositories.Writers;
using MediatR;

namespace Lamba.Identity.Application.Features.Commands.Roles
{
    public class UpdateRoleCommand : BaseAuthorizeRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class UpdateRoleCommandHandler : BaseAuthorizeRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly IRoleWriterRepository _roleWriterRepository;
        private readonly IRoleReaderRepository _roleReaderRepository;

        public UpdateRoleCommandHandler(ICurrentUserAccessor currentUserAccessor, IRoleWriterRepository roleWriterRepository, IRoleReaderRepository roleReaderRepository) : base(currentUserAccessor)
        {
            _roleWriterRepository = roleWriterRepository;
            _roleReaderRepository = roleReaderRepository;
        }

        public override async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleReaderRepository.GetAsync(request.Id, cancellationToken);
            if (role is null) throw new Exception(RoleMessages.RoleNotFound);
            role.SetName(request.Name);
            role.UpdatedUserId = _currentUserAccessor?.GetId();
            _roleWriterRepository.Attach(role);
            _roleWriterRepository.Update(role);
            await _roleWriterRepository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

using Lamba.Identity.Application.Common.Accessors;
using Lamba.Identity.Application.Common.Handlers;
using Lamba.Identity.Application.Infrastructure.Repositories.Writers;
using Lamba.Identity.Domain.Entities;
using MediatR;

namespace Lamba.Identity.Application.Features.Commands.Roles
{
    public class CreateRoleCommand : BaseAuthorizeRequest<Guid>
    {
        public string Name { get; set; } = null!;
    }

    public class CreateRoleCommandHandler : BaseAuthorizeRequestHandler<CreateRoleCommand, Guid>
    {
        private readonly IRoleWriterRepository _roleWriterRepository;

        public CreateRoleCommandHandler(ICurrentUserAccessor currentUserAccessor, IRoleWriterRepository roleWriterRepository) : base(currentUserAccessor)
        {
            _roleWriterRepository = roleWriterRepository;
        }

        public override async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new Role(request.Name, false, false);
            role.CreatedUserId = _currentUserAccessor?.GetId();
            var result = await _roleWriterRepository.AddAsync(role, cancellationToken);
            await _roleWriterRepository.SaveChangesAsync(cancellationToken);
            return result.Entity.Id;
        }
    }
}

using Lamba.Identity.Application.Common.Accessors;
using Lamba.Identity.Application.Common.Handlers;
using Lamba.Identity.Application.Infrastructure.Repositories.Writers;
using Lamba.Identity.Domain.Entities;

namespace Lamba.Identity.Application.Features.Commands.UserRoles
{
    public class AddUserRoleCommand : BaseAuthorizeRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }

    public class AddUserRoleCommandHandler : BaseAuthorizeRequestHandler<AddUserRoleCommand, Guid>
    {
        private readonly IUserRoleWriterRepository _userRoleWriterRepository;

        public AddUserRoleCommandHandler(ICurrentUserAccessor? currentUserAccessor, IUserRoleWriterRepository userRoleWriterRepository) : base(currentUserAccessor)
        {
            _userRoleWriterRepository = userRoleWriterRepository;
        }

        public override async Task<Guid> Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
        {
            var userRole = new UserRole(request.UserId, request.RoleId)
            {
                CreatedUserId = _currentUserAccessor?.GetId()
            };
            await _userRoleWriterRepository.AddAsync(userRole, cancellationToken);
            await _userRoleWriterRepository.SaveChangesAsync(cancellationToken);
            return userRole.Id;
        }
    }
}

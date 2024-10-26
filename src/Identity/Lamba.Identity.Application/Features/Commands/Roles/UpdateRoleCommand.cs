using Lamba.Identity.Application.Common.Accessors;
using Lamba.Identity.Application.Common.Constants;
using Lamba.Identity.Application.Common.Handlers;
using Lamba.Identity.Application.Infrastructure.Repositories.Readers;
using Lamba.Identity.Application.Infrastructure.Repositories.Writers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Identity.Application.Features.Commands.Roles
{
    public class UpdateRoleCommand : BaseAuthorizeRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly IRoleWriterRepository _roleWriterRepository;
        private readonly IRoleReaderRepository _roleReaderRepository;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public UpdateRoleCommandHandler(IRoleWriterRepository roleWriterRepository, IRoleReaderRepository roleReaderRepository, ICurrentUserAccessor currentUserAccessor)
        {
            _roleWriterRepository = roleWriterRepository;
            _roleReaderRepository = roleReaderRepository;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleReaderRepository.GetAsync(request.Id, cancellationToken);
            if (role is null) throw new Exception(RoleMessages.RoleNotFound);
            role.SetName(request.Name);
            role.UpdatedUserId = _currentUserAccessor.GetId();
            _roleWriterRepository.Attach(role);
            _roleWriterRepository.Update(role);
            await _roleWriterRepository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

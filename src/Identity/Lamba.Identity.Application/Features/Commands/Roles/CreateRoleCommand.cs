using Lamba.Identity.Application.Common.Accessors;
using Lamba.Identity.Application.Common.Handlers;
using Lamba.Identity.Application.Infrastructure.Repositories.Writers;
using Lamba.Identity.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Identity.Application.Features.Commands.Roles
{
    public class CreateRoleCommand : BaseAuthorizeRequest<Guid>
    {
        public string Name { get; set; } = null!;
    }

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Guid>
    {
        private readonly IRoleWriterRepository _roleWriterRepository;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public CreateRoleCommandHandler(IRoleWriterRepository roleWriterRepository, ICurrentUserAccessor currentUserAccessor)
        {
            _roleWriterRepository = roleWriterRepository;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new Role(request.Name, false, false);
            var result = await _roleWriterRepository.AddAsync(role, cancellationToken);
            await _roleWriterRepository.SaveChangesAsync(cancellationToken);
            return result.Entity.Id;
        }
    }
}

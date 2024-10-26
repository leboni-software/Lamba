using Lamba.Identity.Application.Common.Handlers;
using Lamba.Identity.Application.Features.Queries.Roles.Dto;
using Lamba.Identity.Application.Infrastructure.Repositories.Readers;
using Microsoft.EntityFrameworkCore;

namespace Lamba.Identity.Application.Features.Queries.Roles
{
    public class GetRolesQuery : BaseAuthorizeRequest<List<GetRoleResponseDto>>
    {

    }

    public class GetRolesQueryHandler : BaseAuthorizeRequestHandler<GetRolesQuery, List<GetRoleResponseDto>>
    {
        private readonly IRoleReaderRepository _roleReaderRepository;

        public GetRolesQueryHandler(IRoleReaderRepository roleReaderRepository) : base()
        {
            _roleReaderRepository = roleReaderRepository;
        }

        public override async Task<List<GetRoleResponseDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            return await _roleReaderRepository.GetQueryable()
                .Select(x => new GetRoleResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsMasterRole = x.IsMasterRole,
                    IsDefaultRole = x.IsDefaultRole,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}

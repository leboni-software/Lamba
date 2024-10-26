using Lamba.Identity.Application.Common.Handlers;
using Lamba.Identity.Application.Features.Queries.Roles.Dto;
using Lamba.Identity.Application.Infrastructure.Repositories.Readers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lamba.Identity.Application.Features.Queries.Roles
{
    public class GetRoleQuery : BaseAuthorizeRequest<GetRoleResponseDto?>
    {
        public Guid Id { get; set; }
    }

    public class GetRoleQueryHandler : BaseAuthorizeRequestHandler<GetRoleQuery, GetRoleResponseDto?>
    {
        private readonly IRoleReaderRepository _roleReaderRepository;

        public GetRoleQueryHandler(IRoleReaderRepository roleReaderRepository) : base()
        {
            _roleReaderRepository = roleReaderRepository;
        }

        public override async Task<GetRoleResponseDto?> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            return await _roleReaderRepository.GetQueryable()
                .Where(x => x.Id == request.Id)
                .Select(x => new GetRoleResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsMasterRole = x.IsMasterRole,
                    IsDefaultRole = x.IsDefaultRole,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}

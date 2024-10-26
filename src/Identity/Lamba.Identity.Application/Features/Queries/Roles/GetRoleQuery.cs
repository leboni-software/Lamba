using Lamba.Identity.Application.Common.Handlers;
using Lamba.Identity.Application.Features.Queries.Roles.Dto;
using Lamba.Identity.Application.Infrastructure.Repositories.Readers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Identity.Application.Features.Queries.Roles
{
    public class GetRoleQuery : BaseAuthorizeRequest<GetRoleResponseDto?>
    {
        public Guid Id { get; set; }
    }

    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, GetRoleResponseDto?>
    {
        private readonly IRoleReaderRepository _roleReaderRepository;

        public GetRoleQueryHandler(IRoleReaderRepository roleReaderRepository)
        {
            _roleReaderRepository = roleReaderRepository;
        }

        public async Task<GetRoleResponseDto?> Handle(GetRoleQuery request, CancellationToken cancellationToken)
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

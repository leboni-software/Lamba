namespace Lamba.Identity.Application.Features.Queries.Roles.Dto
{
    public class GetRoleResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsMasterRole { get; set; }
        public bool IsDefaultRole { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

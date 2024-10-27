namespace Lamba.Identity.Application.Features.Commands.UserRoles.Dto
{
    public class AddUserRoleRequestDto
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}

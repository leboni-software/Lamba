namespace Lamba.Identity.Application.Features.Queries.Users.Dto
{
    public record UserResponseDto
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
    }
}

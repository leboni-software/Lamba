namespace Lamba.Identity.Application.Features.Commands.Authentications.Dto
{
    public class LoginRequestDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}

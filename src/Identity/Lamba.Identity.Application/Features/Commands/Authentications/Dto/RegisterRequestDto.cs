using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Identity.Application.Features.Commands.Authentications.Dto
{
    public class RegisterRequestDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Identity.Application.Features.Commands.Authentications.Dto
{
    public class LoginResponseDto
    {
        public required string Token { get; set; }
    }
}

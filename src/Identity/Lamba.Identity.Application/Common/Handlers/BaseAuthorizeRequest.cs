using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Identity.Application.Common.Handlers
{
    public class BaseAuthorizeRequest<TResponse> : IAuthorizeRequest<TResponse>
    {
        public required List<string> Roles { get; set; }
    }
}

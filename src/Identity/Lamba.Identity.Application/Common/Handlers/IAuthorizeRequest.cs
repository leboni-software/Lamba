using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Identity.Application.Common.Handlers
{
    public interface IAuthorizeRequest<TResponse> : IRequest<TResponse>
    {
        List<string> Roles { get; set; }
    }
    public interface IAuthorizeRequest : IRequest
    {
        List<string> Roles { get; set; }
    }
}

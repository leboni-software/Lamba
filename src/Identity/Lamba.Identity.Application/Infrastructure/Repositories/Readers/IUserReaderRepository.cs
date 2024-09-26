using Lamba.Identity.Domain.Entities;
using Lamba.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Identity.Application.Infrastructure.Repositories.Readers
{
    public interface IUserReaderRepository : IReaderRepository<User, Guid>
    {
    }
}

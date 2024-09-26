using Lamba.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Identity.Application.Infrastructure.Contexts
{
    public interface IIdentityDbContext
    {
        DbSet<User> Users { get; }
    }
}

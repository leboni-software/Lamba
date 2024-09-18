using Lamba.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Domain.Concrete
{
    public class BaseAuditEntity<TKey> : BaseEntity<TKey>, IHasUpdatedAt, IHasDeletedAt
        where TKey : struct
    {
        public virtual DateTime? UpdatedAt { get; set; }
        public virtual DateTime? DeletedAt { get; set; }
    }
}

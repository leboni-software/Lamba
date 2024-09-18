using Lamba.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Domain.Concrete
{
    public class BaseEntity<TKey> : IEntityIdentifier<TKey>, IHasCreatedAt
        where TKey : struct
    {
        [Key]
        public virtual TKey Id { get; set; }
        public virtual DateTime CreatedAt { get; set; }
    }
}

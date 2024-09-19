using Lamba.Domain.Abstract;
using System.ComponentModel.DataAnnotations;

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Domain.Abstract
{
    public interface IHasDeletedUserId<TKey>
    {
        public TKey DeletedUserId { get; set; }
    }
}

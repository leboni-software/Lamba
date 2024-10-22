using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Domain.Abstract
{
    public interface IHasUpdatedUserId<TKey>
    {
        public TKey UpdatedUserId { get; set; }
    }
}

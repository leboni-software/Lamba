using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Domain.Abstract
{
    public interface IHasCreatedUserId<TKey>
    {
        public TKey CreatedUserId { get; set; }
    }
}

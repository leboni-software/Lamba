using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Domain.Abstract
{
    internal interface IHasCreatedAt
    {
        public DateTime CreatedAt { get; set; }
    }
}

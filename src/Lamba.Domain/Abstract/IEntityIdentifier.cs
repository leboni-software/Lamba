using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Domain.Abstract
{
    public interface IEntityIdentifier<TKey> where TKey : struct
    {        
        public TKey Id { get; set; }
    }
}

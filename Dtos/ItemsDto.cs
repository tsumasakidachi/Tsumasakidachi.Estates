using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Tsumasakidachi.Estates.Dtos
{
    [DataContract]
    public class ItemsDto<T>
    {
        [DataMember]
        public IEnumerable<T> items { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Tsumasakidachi.Estates.Dtos
{
    [DataContract]
    public class MarketDto
    {
        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string updated_at { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string order { get; set; }
    }
}

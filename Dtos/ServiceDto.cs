using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Tsumasakidachi.Estates.Dtos
{
    [DataContract]

    public class ServiceDto
    {
        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string updated_at { get; set; }

        [DataMember]
        public string order { get; set; }

        [DataMember]
        public string floor { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public int? area { get; set; }

        [DataMember]
        public string provide_as { get; set; }

        [DataMember]
        public double? cost_per_sq_m { get; set; }

        [DataMember]
        public string unit { get; set; }
    }
}

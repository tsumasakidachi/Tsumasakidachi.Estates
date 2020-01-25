using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Tsumasakidachi.Estates.Dtos
{
    [DataContract]

    public class StructureDto
    {
        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string updated_at { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string order { get; set; }

        [DataMember]
        public string slug { get; set; }

        [DataMember]
        public string comment { get; set; }

        [DataMember]
        public bool is_published { get; set; }

        [DataMember]
        public string[] tag_ids { get; set; }

        [DataMember]
        public string market_id { get; set; }

        [DataMember]
        public MarketDto market { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string completed_at { get; set; }

        [DataMember]
        public string status { get; set; }

        [DataMember]
        public string address { get; set; }

        [DataMember]
        public string server { get; set; }

        [DataMember]
        public string world { get; set; }

        [DataMember]
        public double lat { get; set; }

        [DataMember]
        public double lon { get; set; }

        [DataMember]
        public string eyecatch_url { get; set; }

        [DataMember]
        public ServiceDto[] services { get; set; }
    }
}

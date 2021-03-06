﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Tsumasakidachi.Estates.Dtos
{
    [DataContract]
    public class FileDto
    {
        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string updated_at { get; set; }

        [DataMember]
        public string url { get; set; }
    }
}

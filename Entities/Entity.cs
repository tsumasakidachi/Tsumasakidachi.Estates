using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tsumasakidachi.Estates.Entities
{
    public class Entity
    {
        public string Id { get; set; }
        public DateTime UpdatedAt { get; set; }

        public override bool Equals(object obj)
        {
            var that = obj as Entity;
            return obj != null && that.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}

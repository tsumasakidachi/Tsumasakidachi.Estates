using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tsumasakidachi.Estates.Entities
{
    public class Service : Entity
    {
        public string Order { get; set; }
        public string Floor { get; set; }
        public string Name { get; set; }
        public int? Area { get; set; }
        public string ProvideAs { get; set; }
        public double? CostPerSqM { get; set; } = null;
        public string Grade
        {
            get => GetGrade();
        }
        public int? Value
        {
            get => GetValue();
        }
        public string Unit { get; set; }

        protected int? GetValue()
        {
            int? value = null;

            if (CostPerSqM.HasValue && Area.HasValue)
            {
                value = (int?)Math.Ceiling((CostPerSqM * Area).Value);
            }

            return value;
        }

        protected string GetGrade()
        {
            if (!Value.HasValue)
                return null;
            if (Value.Value > 7500)
                return "VI";
            else if (Value.Value > 2500)
                return "V";
            else if (Value.Value > 800)
                return "IV";
            else if (Value.Value > 400)
                return "III";
            else if (Value.Value > 100)
                return "II";
            else if (Value.Value > 0)
                return "I";
            else
                return "FREE";
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Tsumasakidachi.Estates.Entities
{
    public class Structure : Entity
    {
        public string Name { get; set; }
        public string Order { get; set; }
        public string Slug { get; set; }
        public string Comment { get; set; }
        public bool IsPublished { get; set; }
        public List<string> TagIds { get; set; }
        public string MarketId { get; set; }
        public Market Market { get; set; }
        public string Type { get; set; }
        public string CompletedAt { get; set; }
        public string Status { get; set; }
        public string StatusAndCompletedAt
        {
            get => GetStatusAndCompletedAt();
        }
        public string Address { get; set; }
        public string Server { get; set; }
        public string World { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public Uri EyecatchUrl { get; set; }
        public Uri EyecatchSUrl
        {
            get
            {
                return new Uri(EyecatchUrl?.ToString() + "/s" + Path.GetExtension(EyecatchUrl?.ToString())) ?? null;
            }
        }
        public Uri EyecatchHUrl
        {
            get
            {
                return new Uri(EyecatchUrl?.ToString() + "/h" + Path.GetExtension(EyecatchUrl?.ToString())) ?? null;
            }
        }

        public List<Service> Services { get; set; }
        public string ShowPath {
            get => GetShowPath();
        }

        protected string GetStatusAndCompletedAt()
        {
            string statusAndCompletedAt = Status;

            if(Status == "完成" && !string.IsNullOrEmpty(CompletedAt))
            {
                statusAndCompletedAt += ", " + CompletedAt;
            }

            return statusAndCompletedAt;
        }

        protected string GetShowPath()
        {
            return "project/" + Slug;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentitySample.Models
{
    public class Events
    {
        public int EventsId { get; set; }
        public string Name { get; set; }
        public DateTime EventDateTime { get; set; }
        public string Details { get; set; }
        public ApplicationUser User { get; set; }
        public Location EventLocation { get; set; }
    }
}
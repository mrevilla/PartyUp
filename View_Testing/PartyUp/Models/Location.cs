using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentitySample.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string Address_1 { get; set; }
        public string Address_2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public virtual ICollection<Events> Events { get; set; }
    }
}
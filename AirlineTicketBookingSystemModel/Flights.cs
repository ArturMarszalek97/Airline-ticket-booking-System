using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTicketBookingSystemModel
{
    public class Flights
    {
        public string _id { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public AirPort airPortFrom { get; set; }
        public AirPort airPortTo { get; set; }
        public int price { get; set; }
        public List<string> details { get; set; }
        public List<Link> links { get; set; }
        public int code { get; set; }
    }
}

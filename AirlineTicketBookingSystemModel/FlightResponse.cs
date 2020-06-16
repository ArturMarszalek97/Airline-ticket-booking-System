using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTicketBookingSystemModel
{
    public class FlightResponse
    {
        public string _id { get; set; }
        public Flights flight { get; set; }
        public User userDTO { get; set; }
        public int code { get; set; }
        public List<string> details { get; set; }
    }
}

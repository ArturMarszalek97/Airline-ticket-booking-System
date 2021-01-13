using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTicketBookingSystemModel
{
    public class Ticket
    {
        public string _id { get; set; }
        public Flights flight { get; set; }
        public int code { get; set; }
    }
}

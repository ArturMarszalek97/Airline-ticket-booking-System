using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTicketBookingSystemModel
{
    public class DepartingPlanes
    {
        public string state { get; set; }
        public List<City> cities { get; set; }
        public List<string> details { get; set; }
    }
}

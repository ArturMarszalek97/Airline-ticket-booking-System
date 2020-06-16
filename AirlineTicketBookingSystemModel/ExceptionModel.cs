using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTicketBookingSystemModel
{
    [Serializable]
    public class ExceptionModel : Exception
    {
        public ExceptionModel(string message) : base(message)
        {

        }
    }
}

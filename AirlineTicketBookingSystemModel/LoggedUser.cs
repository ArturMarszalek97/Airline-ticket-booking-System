using System.Collections.Generic;

namespace AirlineTicketBookingSystemModel
{
    public class LoggedUser
    {
        public string accessToken { get; set; }
        public string tokenType { get; set; }
        public string id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public List<string> roles { get; set; }
        public List<string> details { get; set; }
    }
}

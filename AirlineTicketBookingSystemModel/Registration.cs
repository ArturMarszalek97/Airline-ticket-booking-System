using System.Collections.Generic;

namespace AirlineTicketBookingSystemModel
{
    public class Registration
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public List<string> details { get; set; }

        public Registration(string firstName, string lastName, string username, string email, string password)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.username = username;
            this.email = email;
            this.password = password;
        }
    }
}

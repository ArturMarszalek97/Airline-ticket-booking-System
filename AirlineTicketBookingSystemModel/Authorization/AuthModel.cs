namespace AirlineTicketBookingSystemModel.Authorization
{
    public class AuthModel
    {
        public string username { get; set; }
        public string password { get; set; }

        public AuthModel(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}

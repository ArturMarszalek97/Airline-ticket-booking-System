namespace AirlineTicketBookingSystem
{
    public class StringContainer
    {
        public static string port = string.Empty;

        public StringContainer(string port)
        {
            Authorization = $"http://localhost:{port}/api/auth/signin"; // POST
            Register = $"http://localhost:{port}/api/auth/signup"; // POST
            DepartingPlanes = $"http://localhost:{port}/countries/departingPlanes"; // GET
            AirplanesArriving = $"http://localhost:{port}/countries/airplanesArriving?"; AirplanesArriving += "Country={0}&City={1}"; // GET
            Flights = $"http://localhost:{port}/flights/locations?"; Flights += "countryFrom={0}&cityFrom={1}&countryTo={2}&cityTo={3}&date={4}"; //GET
            Tickets = $"http://localhost:{port}/tickets"; // POST
            Reservation = $"http://localhost:{port}/tickets/reservation"; Reservation += "/{0}"; // GET
            RemindPasssword = $"http://localhost:{port}/users/sendEmail?"; RemindPasssword += "email ={0}"; // PATCH
            Code = $"http://localhost:{port}/users?"; Code += "code={0}&email={1}"; // GET
            ChangePassword = $"http://localhost:{port}/users/resetPassword?"; ChangePassword += "password={0}&email={1}"; // POST
            FlightsFromDate = $"http://localhost:{port}/"; FlightsFromDate += "flights?date={0}"; // GET
            CreateFlight = $"http://localhost:{port}/flights"; // POST
            UserTickets = $"http://localhost:{port}/"; UserTickets += "tickets?username={0}"; // GET
            CheckFlight = $"http://localhost:{port}/"; CheckFlight += "flights/{0}/tickets"; // GET
            EditFlight = $"http://localhost:{port}/"; EditFlight += "flights/{0}"; // POST
            ShowDeleteFlight = $"http://localhost:{port}/"; ShowDeleteFlight += "flights/{0}"; // GET or DELETE
        }

        public static string Authorization = $"http://localhost:{port}/api/auth/signin"; // POST

        public static string Register = $"http://localhost:{port}/api/auth/signup"; // POST

        public static string DepartingPlanes = $"http://localhost:{port}/countries/departingPlanes"; // GET

        public static string AirplanesArriving = $"http://localhost:{port}/countries/airplanesArriving?Country={0}&City={1}"; // GET

        public static string Flights = $"http://localhost:{port}/flights/locations?countryFrom={0}&cityFrom={1}&countryTo={2}&cityTo={3}&date={4}"; //GET

        public static string Tickets = $"http://localhost:{port}/tickets"; // POST

        public static string Reservation = $"http://localhost:{port}/tickets/reservation/{0}"; // GET

        public static string RemindPasssword = $"http://localhost:{port}/users/sendEmail?email={0}"; // PATCH

        public static string Code = $"http://localhost:{port}/users?code={0}&email={1}"; // GET

        public static string ChangePassword = $"http://localhost:{port}/users/resetPassword?password={0}&email={1}"; // POST

        public static string FlightsFromDate = $"http://localhost:{port}/flights?date={0}"; // GET

        public static string CreateFlight = $"http://localhost:{port}/flights"; // POST

        public static string UserTickets = $"http://localhost:{port}/tickets?username={0}"; // GET

        public static string CheckFlight = $"http://localhost:{port}/flights/{0}/tickets"; // GET

        public static string EditFlight = $"http://localhost:{port}/flights/{0}"; // POST

        public static string ShowDeleteFlight = $"http://localhost:{port}/flights/{0}"; // GET or DELETE

        // Test in HTTP monitor
        //public const string Authorization = "http://localhost:9999/api/auth/signin"; // POST

        //public const string Register = "http://localhost:9999/api/auth/signup"; // POST

        //public const string DepartingPlanes = "http://localhost:9999/countries/departingPlanes"; // GET

        //public const string AirplanesArriving = "http://localhost:9999/countries/airplanesArriving?Country={0}&City={1}"; // GET

        //public const string Flights = "http://localhost:9999/flights/locations?countryFrom={0}&cityFrom={1}&countryTo={2}&cityTo={3}&date={4}"; //GET

        //public const string Tickets = "http://localhost:9999/tickets"; // POST

        //public const string Reservation = "http://localhost:9999/tickets/reservation/{0}"; // GET

        //public const string RemindPasssword = "http://localhost:9999/users/sendEmail?email={0}"; // PATCH

        //public const string Code = "http://localhost:9999/users?code={0}&email={1}"; // GET

        //public const string ChangePassword = "http://localhost:9999/users/resetPassword?password={0}&email={1}"; // POST

        //public const string FlightsFromDate = "http://localhost:9999/flights?date={0}"; // GET

        //public const string CreateFlight = "http://localhost:9999/flights"; // POST
    }
}

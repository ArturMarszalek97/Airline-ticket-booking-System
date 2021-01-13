namespace AirlineTicketBookingSystem
{
    public class StringContainer
    {
        public const string Authorization = "http://localhost:8080/api/auth/signin"; // POST

        public const string Register = "http://localhost:8080/api/auth/signup"; // POST

        public const string DepartingPlanes = "http://localhost:8080/countries/departingPlanes"; // GET

        public const string AirplanesArriving = "http://localhost:8080/countries/airplanesArriving?Country={0}&City={1}"; // GET

        public const string Flights = "http://localhost:8080/flights/locations?countryFrom={0}&cityFrom={1}&countryTo={2}&cityTo={3}&date={4}"; //GET

        public const string Tickets = "http://localhost:8080/tickets"; // POST

        public const string Reservation = "http://localhost:8080/tickets/reservation/{0}"; // GET

        public const string RemindPasssword = "http://localhost:8080/users/sendEmail?email={0}"; // PATCH

        public const string Code = "http://localhost:8080/users?code={0}&email={1}"; // GET

        public const string ChangePassword = "http://localhost:8080/users/resetPassword?password={0}&email={1}"; // POST

        public const string FlightsFromDate = "http://localhost:8080/flights?date={0}"; // GET

        public const string CreateFlight = "http://localhost:8080/flights"; // POST

        public const string UserTickets = "http://localhost:8080/tickets?username={0}"; // GET

        public const string CheckFlight = "http://localhost:8080/flights/{0}/tickets"; // GET

        public const string EditFlight = "http://localhost:8080/flights/{0}"; // POST

        public const string ShowDeleteFlight = "http://localhost:8080/flights/{0}"; // GET or DELETE

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

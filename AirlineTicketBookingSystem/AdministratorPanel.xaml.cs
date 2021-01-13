using System.Windows;

namespace AirlineTicketBookingSystem
{
    /// <summary>
    /// Interaction logic for AdministratorPanel.xaml
    /// </summary>
    public partial class AdministratorPanel : Window
    {
        public AdministratorPanel()
        {
            InitializeComponent();
        }

        private void FlightManagement_Click(object sender, RoutedEventArgs e)
        {
            AirlineTicketBookingSystem.FlightManagement flightManagement = new FlightManagement();
            flightManagement.Show();

            this.Close();
        }

        private void TicketReview_Click(object sender, RoutedEventArgs e)
        {
            UserTicketsManagement userTicketsManagement = new UserTicketsManagement();
            userTicketsManagement.Show();

            this.Close();
        }
    }
}

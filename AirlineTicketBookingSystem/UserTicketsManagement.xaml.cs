using AirlineTicketBookingSystemModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AirlineTicketBookingSystem
{
    /// <summary>
    /// Interaction logic for UserTicketsManagement.xaml
    /// </summary>
    public partial class UserTicketsManagement : Window
    {
        HttpClient client;

        public UserTicketsManagement()
        {
            InitializeComponent();
            this.client = ClientHelper.GetClient();
        }

        private async void SearchFlights_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var listOfFlights = await Tickets();
                this.listView.ItemsSource = listOfFlights;
                this.count.Content = listOfFlights.Count;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<List<Ticket>> Tickets()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Format(StringContainer.UserTickets, this.login.Text));
                string responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var badResult = JsonConvert.DeserializeObject<ErrorModel>(responseBody);
                    throw new ExceptionModel(badResult.details[0]);
                }

                return JsonConvert.DeserializeObject<List<Ticket>>(responseBody);
            }
            catch (ExceptionModel ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private async void DeleteFlight_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć ten bilet?", "Uwaga", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            switch(result)
            {
                case MessageBoxResult.Yes:
                    {
                        try
                        {
                            var selectedTicket = (Ticket)this.listView.SelectedItem;

                            HttpResponseMessage response = await client.DeleteAsync($"{StringContainer.Tickets}/{selectedTicket._id}");
                            string responseBody = await response.Content.ReadAsStringAsync();

                            var deseriallizeObject = JsonConvert.DeserializeObject<Message>(responseBody);

                            if (!response.IsSuccessStatusCode)
                                throw new ExceptionModel(deseriallizeObject.details[0]);

                            MessageBox.Show(deseriallizeObject.message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                            SearchFlights_Click(sender, e);
                        }
                        catch (ExceptionModel ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    } break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private async void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedTicket = (Ticket)this.listView.SelectedItem;

            DetailsView detailsView = new DetailsView(selectedTicket);
            detailsView.Show();

            SearchFlights_Click(sender, e);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AdministratorPanel administratorPanel = new AdministratorPanel();
            administratorPanel.Show();

            this.Close();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();

            this.Close();
        }
    }
}

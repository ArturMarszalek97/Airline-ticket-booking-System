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
    /// Interaction logic for FlightManagement.xaml
    /// </summary>
    public partial class FlightManagement : Window
    {
        HttpClient client;

        public FlightManagement()
        {
            InitializeComponent();
            this.client = ClientHelper.GetClient();
        }

        private async void SearchFlights_Click(object sender, RoutedEventArgs e)
        {
            var date = Convert.ToDateTime(this.date.Text).ToString("yyyy-MM-dd");

            try
            {
                var listOfFlights = await Flights(date);
                this.listView.ItemsSource = listOfFlights;
                this.count.Content = listOfFlights.Count;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<List<Flights>> Flights(string date)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Format(StringContainer.FlightsFromDate, date));
                string responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var badResult = JsonConvert.DeserializeObject<ErrorModel>(responseBody);
                    throw new ExceptionModel(badResult.details[0]);
                }

                return JsonConvert.DeserializeObject<List<Flights>>(responseBody);
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

        private async void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var ticket = (Flights)this.listView.SelectedItem;

                HttpResponseMessage response = await client.GetAsync(String.Format(StringContainer.ShowDeleteFlight, ticket._id));
                string responseBody = await response.Content.ReadAsStringAsync();

                var deseriallizeObject = JsonConvert.DeserializeObject<Flights>(responseBody);

                if (!response.IsSuccessStatusCode)
                    throw new ExceptionModel(deseriallizeObject.details[0]);

                DetailsView detailsView = new DetailsView(deseriallizeObject);
                detailsView.Show();
            }
            catch (ExceptionModel ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateFlight_Click(object sender, RoutedEventArgs e)
        {
            AirlineTicketBookingSystem.CreateFlight createFlight = new CreateFlight();
            createFlight.Show();
        }

        private async void DeleteFlight_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedFlight = (Flights)this.listView.SelectedItem;

                HttpResponseMessage response = await client.DeleteAsync(String.Format(StringContainer.ShowDeleteFlight, selectedFlight._id));
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
        }

        private async void EditFlight_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
                var selectedFlight = (Flights)this.listView.SelectedItem;

                HttpResponseMessage response = await client.GetAsync(String.Format(StringContainer.CheckFlight, selectedFlight._id));
                string responseBody = await response.Content.ReadAsStringAsync();

                

                if (!response.IsSuccessStatusCode)
                {
                    var deseriallizeObject = JsonConvert.DeserializeObject<Flights>(responseBody);
                    throw new ExceptionModel(deseriallizeObject.details[0]);
                }

               

                AirlineTicketBookingSystem.EditFlight editFlight = new EditFlight(selectedFlight);
                if ((bool)editFlight.ShowDialog())
                {
                    SearchFlights_Click(sender, e);
                }
            }
            catch (ExceptionModel ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

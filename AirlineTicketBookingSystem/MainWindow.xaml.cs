using AirlineTicketBookingSystemModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AirlineTicketBookingSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client;
        List<DepartingPlanes> listOfDepartingPlanes;
        List<string> listOfCountryFrom = new List<string>();
        List<AirplanesArriving> listOfAirplanesArriving = new List<AirplanesArriving>();

        public MainWindow()
        {
            InitializeComponent();
            this.client = ClientHelper.GetClient();

            LockComboBoxes();
            SetCountryFromComboBox();
        }

        private void LockComboBoxes()
        {
            this.cityFrom.IsEnabled = false;
            this.date.IsEnabled = false;
            this.countryTo.IsEnabled = false;
            this.cityTo.IsEnabled = false;
        }

        private async Task SetCountryFromComboBox()
        {
            await GetDepartingPlanes();

            this.listOfCountryFrom.Clear();

            foreach (var item in listOfDepartingPlanes)
            {
                this.countryFrom.Items.Add(item.state);
            }
        }

        public async Task GetDepartingPlanes()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(StringContainer.DepartingPlanes);
                string responseBody = await response.Content.ReadAsStringAsync();

                listOfDepartingPlanes = JsonConvert.DeserializeObject<List<DepartingPlanes>>(responseBody);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void countryFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.countryFrom.SelectedItem == null)
            {
                await GetDepartingPlanes();
                return;
            }

            ClearFields();

            foreach (var item in listOfDepartingPlanes)
            {
                if (item.state == this.countryFrom.SelectedItem.ToString())
                {
                    foreach (var cities in item.cities)
                    {
                        this.cityFrom.Items.Add(cities.city);
                    }
                }
            }
        }

        private void ClearFields()
        {
            this.listView.ItemsSource = null;

            this.cityFrom.IsEnabled = true;

            this.date.SelectedDate = null;
            this.date.IsEnabled = false;

            this.countryTo.SelectedItem = null;
            this.countryTo.IsEnabled = false;

            this.cityTo.SelectedItem = null;
            this.cityTo.IsEnabled = false;

            this.cityFrom.SelectedItem = null;
            this.cityFrom.Items.Clear();
        }

        private async void cityFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.listView.ItemsSource = null;

            this.date.SelectedDate = null;

            if (this.cityFrom.SelectedItem == null)
            {
                this.countryTo.IsEnabled = false;
                this.date.IsEnabled = false;
                this.cityTo.IsEnabled = false;
            }
            else
            {
                this.date.IsEnabled = true;
                this.countryTo.IsEnabled = false;
                this.cityTo.IsEnabled = false;
            }
            
            

            this.countryTo.SelectedItem = null;
            this.countryTo.Items.Clear();

            if (this.cityFrom.SelectedItem != null)
            {
                try
                {
                    listOfAirplanesArriving = await GetAirplanesArriving(this.countryFrom.SelectedItem.ToString(), this.cityFrom.SelectedItem.ToString());

                    foreach (var item in listOfAirplanesArriving)
                    {
                        this.countryTo.Items.Add(item.state);
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
        }

        private async Task<List<AirplanesArriving>> GetAirplanesArriving(string country, string city)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Format(StringContainer.AirplanesArriving, country, city));
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<AirplanesArriving>>(responseBody);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.date.SelectedDate == null)
            {
                this.countryTo.IsEnabled = false;
            }
            else
            {
                this.countryTo.IsEnabled = true;
            }
        }

        private void countryTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.listView.ItemsSource = null;

            if (this.countryTo.SelectedItem == null)
            {
                this.cityTo.IsEnabled = false;
            }
            else
            {
                this.cityTo.IsEnabled = true;
            }
            

            this.cityTo.SelectedItem = null;
            this.cityTo.Items.Clear();

            foreach (var item in listOfAirplanesArriving)
            {
                if (this.countryTo.SelectedItem != null && item.state == this.countryTo.SelectedItem.ToString())
                {
                    foreach (var city in item.cities)
                    {
                        this.cityTo.Items.Add(city.city);
                    }
                }
            }
        }

        private async void cityTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.listView.ItemsSource = null;
            await ShowPotencialOptions();
        }

        private async Task ShowPotencialOptions()
        {
            if (this.countryFrom.SelectedItem != null && this.cityFrom.SelectedItem != null
                && this.countryTo.SelectedItem != null && this.cityTo.SelectedItem != null && this.date.SelectedDate != null)
            {
                var date = Convert.ToDateTime(this.date.Text).ToString("yyyy-MM-dd");

                try
                {
                    this.listView.ItemsSource = await Flights(date);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }

        private async Task<List<Flights>> Flights(string date)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Format(StringContainer.Flights,
                                                                                    this.countryFrom.SelectedItem.ToString(),
                                                                                    this.cityFrom.SelectedItem.ToString(),
                                                                                    this.countryTo.SelectedItem.ToString(),
                                                                                    this.cityTo.SelectedItem.ToString(),
                                                                                    date));
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
                ClearFields();
                this.cityFrom.IsEnabled = false;
                this.countryFrom.SelectedItem = null;
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private async void book_Click(object sender, RoutedEventArgs e)
        {
            var flight = (Flights)this.listView.SelectedItem;

            if (flight != null)
            {
                try
                {
                    var ticket = await BookTicket(flight);

                    SummaryView summaryView = new SummaryView(ticket);
                    summaryView.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async Task<FlightResponse> BookTicket(Flights flight)
        {
            try
            {
                var json = JsonConvert.SerializeObject(flight);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PostAsync(StringContainer.Tickets, content);
                string responseBody = await responseMessage.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<FlightResponse>(responseBody);

                if (!responseMessage.IsSuccessStatusCode)
                    throw new ExceptionModel(result.details[0]);

                return result;
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

        private async void SearchTicket_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ticketCode.Text) && int.TryParse(this.ticketCode.Text, out int result))
            {
                try
                {
                    var ticket = await CheckTicket(result);

                    if (ticket == null)
                        return;

                    SummaryView summaryView = new SummaryView(ticket);
                    summaryView.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private async Task<FlightResponse> CheckTicket(int result)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Format(StringContainer.Reservation, result));
                string responseBody = await response.Content.ReadAsStringAsync();

                var deseriallizeObject = JsonConvert.DeserializeObject<FlightResponse>(responseBody);

                if (!response.IsSuccessStatusCode)
                    throw new ExceptionModel(deseriallizeObject.details[0]);

                return deseriallizeObject;
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

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();

            this.Close();
        }
    }
}

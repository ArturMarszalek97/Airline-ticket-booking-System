using AirlineTicketBookingSystemModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AirlineTicketBookingSystem
{
    /// <summary>
    /// Interaction logic for EditFlight.xaml
    /// </summary>
    public partial class EditFlight : Window
    {
        HttpClient client;
        Flights flight;

        public EditFlight(AirlineTicketBookingSystemModel.Flights deseriallizeObject)
        {
            InitializeComponent();
            this.client = ClientHelper.GetClient();
            this.flight = deseriallizeObject;

            InitFields();
        }

        private void InitFields()
        {
            this.date.SelectedDate = DateTime.Parse(flight.date);
            this.time.Text = flight.time;
            this.price.Text = flight.price.ToString();
            this.countryFrom.Text = flight.airPortFrom.country;
            this.cityFrom.Text = flight.airPortFrom.city;
            this.airplaneNameFrom.Text = flight.airPortFrom.airPortName;
            this.countryTo.Text = flight.airPortTo.country;
            this.cityTo.Text = flight.airPortTo.city;
            this.airplaneNameTo.Text = flight.airPortTo.airPortName;
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var date = Convert.ToDateTime(this.date.Text).ToString("yyyy-MM-dd");
                var newFlight = new Flights()
                {
                    date = date,
                    time = this.time.Text,
                    airPortFrom = new AirPort()
                    {
                        country = this.countryFrom.Text,
                        city = this.cityFrom.Text,
                        airPortName = this.airplaneNameFrom.Text
                    },
                    airPortTo = new AirPort()
                    {
                        country = this.countryTo.Text,
                        city = this.cityTo.Text,
                        airPortName = this.airplaneNameTo.Text
                    },
                    price = Convert.ToInt32(this.price.Text)
                };

                var json = JsonConvert.SerializeObject(newFlight);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PostAsync(String.Format(StringContainer.EditFlight, flight._id), content);
                string responseBody = await responseMessage.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<Message>(responseBody);

                if (!responseMessage.IsSuccessStatusCode)
                    throw new ExceptionModel(result.details[0]);

                MessageBox.Show(result.message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                this.DialogResult = true;
                this.Close();
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
}

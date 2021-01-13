using AirlineTicketBookingSystemModel;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace AirlineTicketBookingSystem
{
    /// <summary>
    /// Interaction logic for CreateFlight.xaml
    /// </summary>
    public partial class CreateFlight : Window
    {
        HttpClient client;

        public CreateFlight()
        {
            InitializeComponent();
            this.client = ClientHelper.GetClient();
        }

        private async void Create_Click(object sender, RoutedEventArgs e)
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

                HttpResponseMessage responseMessage = await client.PostAsync(StringContainer.CreateFlight, content);
                string responseBody = await responseMessage.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<Message>(responseBody);

                if (!responseMessage.IsSuccessStatusCode)
                    throw new ExceptionModel(result.details[0]);

                MessageBox.Show(result.message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);

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

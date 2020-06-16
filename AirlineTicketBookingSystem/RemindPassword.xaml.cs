using AirlineTicketBookingSystemModel;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace AirlineTicketBookingSystem
{
    /// <summary>
    /// Interaction logic for RemindPassword.xaml
    /// </summary>
    public partial class RemindPassword : Window
    {
        HttpClient client;

        public RemindPassword()
        {
            InitializeComponent();
            this.client = ClientHelper.GetClient();
        }

        private async void GenerateCode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var content = new StringContent("", Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(String.Format(StringContainer.RemindPasssword, this.email.Text), content);
                string responseBody = await response.Content.ReadAsStringAsync();

                var message = JsonConvert.DeserializeObject<Message>(responseBody);

                if (!response.IsSuccessStatusCode)
                    throw new ExceptionModel(message.details[0]);

                MessageBox.Show(message.message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                RemindPasswordCheckCode checkCode = new RemindPasswordCheckCode(this.email.Text);
                checkCode.Show();

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

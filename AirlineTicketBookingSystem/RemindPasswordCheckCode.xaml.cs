using AirlineTicketBookingSystemModel;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Windows;

namespace AirlineTicketBookingSystem
{
    /// <summary>
    /// Interaction logic for RemindPasswordCheckCode.xaml
    /// </summary>
    public partial class RemindPasswordCheckCode : Window
    {
        HttpClient client;
        string mail;

        public RemindPasswordCheckCode(string mail)
        {
            InitializeComponent();
            this.client = ClientHelper.GetClient();
            this.mail = mail;
        }

        private async void Valid_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(String.Format(StringContainer.Code, this.code.Text, mail));
                string responseBody = await response.Content.ReadAsStringAsync();

                var message = JsonConvert.DeserializeObject<Message>(responseBody);

                if (!response.IsSuccessStatusCode)
                    throw new ExceptionModel(message.details[0]);

                MessageBox.Show(message.message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                NewPassword newPassword = new NewPassword(mail);
                newPassword.Show();

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

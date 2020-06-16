using AirlineTicketBookingSystemModel;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace AirlineTicketBookingSystem
{
    /// <summary>
    /// Interaction logic for NewPassword.xaml
    /// </summary>
    public partial class NewPassword : Window
    {
        HttpClient client;
        string mail;

        public NewPassword(string mail)
        {
            InitializeComponent();
            this.client = ClientHelper.GetClient();
            this.mail = mail;
        }

        private async void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var content = new StringContent("", Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(String.Format(StringContainer.ChangePassword, this.password.Password, mail), content);
                string responseBody = await response.Content.ReadAsStringAsync();

                var message = JsonConvert.DeserializeObject<Message>(responseBody);

                if (!response.IsSuccessStatusCode)
                    throw new ExceptionModel(message.details[0]);

                MessageBox.Show(message.message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                LoginView loginView = new LoginView();
                loginView.Show();

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

using AirlineTicketBookingSystemModel;
using AirlineTicketBookingSystemModel.Authorization;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace AirlineTicketBookingSystem
{
    /// <summary>
    /// Interaction logic for SignIn_or_Register.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        HttpClient client;

        public LoginView()
        {
            InitializeComponent();
            ClientHelper.InitHttpClient();
            this.client = ClientHelper.GetClient();
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var json = JsonConvert.SerializeObject(new AuthModel(this.login.Text, this.password.Password));
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PostAsync(StringContainer.Authorization, content);
                string responseBody = await responseMessage.Content.ReadAsStringAsync();

                var response = JsonConvert.DeserializeObject<LoggedUser>(responseBody);

                if (!responseMessage.IsSuccessStatusCode)
                    throw new ExceptionModel(response.details[0]);

                ClientHelper.SetHeaders(response.tokenType, response.accessToken);

                MainWindow window = new MainWindow();
                window.Show();

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

        private void remindPassword_Click(object sender, RoutedEventArgs e)
        {
            RemindPassword remindPassword = new RemindPassword();
            remindPassword.Show();

            this.Close();
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationView registrationView = new RegistrationView();
            registrationView.Show();

            this.Close();
        }
    }
}

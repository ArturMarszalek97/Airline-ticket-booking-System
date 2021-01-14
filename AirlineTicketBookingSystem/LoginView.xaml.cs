using AirlineTicketBookingSystemModel;
using AirlineTicketBookingSystemModel.Authorization;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;

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
            ShowPortField();
        }

        private void ShowPortField()
        {
            if (!string.IsNullOrEmpty(StringContainer.port))
            {
                this.PortName.Visibility = Visibility.Hidden;
                this.Port.Visibility = Visibility.Hidden;

                this.login.IsEnabled = true;
                this.password.IsEnabled = true;
                this.loginButton.IsEnabled = true;
                this.remindPassword.IsEnabled = true;
                this.registerButton.IsEnabled = true;
            }
            else
            {
                this.PortName.Visibility = Visibility.Visible;
                this.Port.Visibility = Visibility.Visible;

                this.login.IsEnabled = false;
                this.password.IsEnabled = false;
                this.loginButton.IsEnabled = false;
                this.remindPassword.IsEnabled = false;
                this.registerButton.IsEnabled = false;
            }
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(StringContainer.port))
                {
                    StringContainer.port = this.Port.Text;
                    var sc = new StringContainer(this.Port.Text);
                }
                
                var json = JsonConvert.SerializeObject(new AuthModel(this.login.Text, this.password.Password));
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PostAsync(StringContainer.Authorization, content);
                string responseBody = await responseMessage.Content.ReadAsStringAsync();

                var response = JsonConvert.DeserializeObject<LoggedUser>(responseBody);

                if (!responseMessage.IsSuccessStatusCode)
                    throw new ExceptionModel(response.details[0]);

                LoggedUserHelper.loggedUser = response;
                ClientHelper.SetHeaders(response.tokenType, response.accessToken);

                if (response.roles[0] == "ROLE_ADMIN")
                {
                    AdministratorPanel administratorPanel = new AdministratorPanel();
                    administratorPanel.Show();

                    this.Close();
                }
                else
                {
                    MainWindow window = new MainWindow();
                    window.Show();

                    this.Close();
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

        private void Port_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (Convert.ToInt32(textBox.Text) >= 1000 && Convert.ToInt32(textBox.Text) <= 9999)
            {
                StringContainer.port = this.Port.Text;
                var sc = new StringContainer(this.Port.Text);

                this.login.IsEnabled = true;
                this.password.IsEnabled = true;
                this.loginButton.IsEnabled = true;
                this.remindPassword.IsEnabled = true;
                this.registerButton.IsEnabled = true;
            }
        }
    }
}

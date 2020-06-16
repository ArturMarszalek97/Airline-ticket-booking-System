using AirlineTicketBookingSystemModel;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AirlineTicketBookingSystem
{
    /// <summary>
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : Window
    {
        HttpClient client;

        public RegistrationView()
        {
            InitializeComponent();
            this.client = ClientHelper.GetClient();
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newUser = new Registration(this.name.Text, this.surname.Text, this.login.Text, this.email.Text, this.password.Password);
                var register = await RegisterNewUser(newUser);

                if (register == null)
                    return;

                LoginView loginView = new LoginView();
                loginView.Show();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<Registration> RegisterNewUser(Registration newUser)
        {
            try
            {
                var rejestracjaJSON = JsonConvert.SerializeObject(newUser);
                var rejestracjaContent = new StringContent(rejestracjaJSON, Encoding.UTF8, "application/json");

                HttpResponseMessage rejestracjaResponseMessage = await client.PostAsync(StringContainer.Register, rejestracjaContent);
                string rejestracjaResponseBody = await rejestracjaResponseMessage.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<Registration>(rejestracjaResponseBody);

                if (!rejestracjaResponseMessage.IsSuccessStatusCode)
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
    }
}

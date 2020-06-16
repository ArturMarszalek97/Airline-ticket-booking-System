using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTicketBookingSystem
{
    public static class ClientHelper
    {
        private static HttpClient client;

        public static void InitHttpClient()
        {
            client = new HttpClient();

            // trust any certificate
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            //client.BaseAddress = new Uri(AuthorizationServer);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static void SetHeaders(string tokenType, string accessToken)
        {
            client.DefaultRequestHeaders.Add("Authorization", $"{tokenType} {accessToken}");
        }

        public static HttpClient GetClient()
        {
            return client;
        }
    }
}

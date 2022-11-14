using System;
using System.Net.Http;
using WhatsOnCampus.Services;

namespace WhatsOnCampus.Services
{
    public class WebClientService : IWebClientService
    {
        public async Task<string> GetTwitterFeed(string uri)
        {
            try
            {
                // HttpClient object
                HttpClient client;
                client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"AAAAAAAAAAAAAAAAAAAAAJhRigEAAAAAlob0KHkOOO3dBDnChZ9OTIW0xi0%3DptEQF6vEOmvAdXQyi8cKtj3HYX0wprnG8KZFgUEoPtINNMtqoZ");

                // Calling actual get method on the desired uri
                HttpResponseMessage response = await client.GetAsync(uri);
                // Checks if response has success status code or not
                return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;
            }
            catch
            {
                return null;
            }
        }
    }
}


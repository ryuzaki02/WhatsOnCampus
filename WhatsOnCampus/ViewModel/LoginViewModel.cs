using System;
using Microsoft.Identity.Client;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using WhatsOnCampus.Controls;
using WhatsOnCampus.Model;
using WhatsOnCampus.MSAL;
using WhatsOnCampus.Views;

namespace WhatsOnCampus.ViewModel
{ 
    public struct UserAuthentication
    {
        public string userDetails;
        public string error;        

        public UserAuthentication(string userDetails, string error) : this()
        {
            this.userDetails = userDetails;
            this.error = error;
        }
    }

	public partial class LoginViewModel: BaseViewModel
	{
        [ICommand]
        async void Login()
        {
            UserAuthentication authentication = await AuthenticateMicrosoft();

            if (string.IsNullOrWhiteSpace(authentication.error))
            {
                if (Preferences.ContainsKey(nameof(App.user)))
                {
                    Preferences.Remove(nameof(App.user));
                }                
                Preferences.Set(nameof(App.user), authentication.userDetails);
                User user = JsonConvert.DeserializeObject<User>(authentication.userDetails);
                App.user = user;
                Preferences.Set("User", JsonConvert.SerializeObject(user));
                AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
                await Shell.Current.GoToAsync($"//{nameof(FeedPage)}");
            }
            else
            {

            }            
        }

        public LoginViewModel()
        {
           
        }

		public async Task<UserAuthentication> AuthenticateMicrosoft()
		{
            // ----- Actual microsoft authentication     
            try
            {
                PCAWrapper.Instance.UseEmbedded = false;// this.useEmbedded.IsChecked;
                                                        // First attempt silent login, which checks the cache for an existing valid token.
                                                        // If this is very first time or user has signed out, it will throw MsalUiRequiredException
                AuthenticationResult result = await PCAWrapper.Instance.AcquireTokenSilentAsync(Constants.Scopes).ConfigureAwait(false);

                // call Web API to get the data
                UserAuthentication data = await CallWebAPIWithToken(result).ConfigureAwait(false);

                // show the data
                //await ShowMessage("AcquireTokenSilent call", data).ConfigureAwait(false);
                return data;

            }
            catch (MsalUiRequiredException)
            {
                // This executes UI interaction to obtain token
                AuthenticationResult result = await PCAWrapper.Instance.AcquireTokenInteractiveAsync(Constants.Scopes).ConfigureAwait(false);

                // call Web API to get the data
                UserAuthentication data = await CallWebAPIWithToken(result).ConfigureAwait(false);

                // show the data
                //await ShowMessage("AcquireTokenInteractive call", data).ConfigureAwait(false);
                return data;
                
            }
            catch (Exception ex)
            {
                //await ShowMessage("Exception in AcquireTokenSilent", ex.Message).ConfigureAwait(false);
                return new UserAuthentication(null, ex.Message);
            }
            
        }

        // Actual Microsoft authentication
        // Call the web api. The code is left in the Ux file for easy to see.
        private async Task<UserAuthentication> CallWebAPIWithToken(AuthenticationResult authResult)
        {
            try
            {
                //get data from API
                HttpClient client = new HttpClient();
                // create the request
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me");

                // ** Add Authorization Header **
                message.Headers.Add("Authorization", authResult.CreateAuthorizationHeader());

                // send the request and return the response
                HttpResponseMessage response = await client.SendAsync(message).ConfigureAwait(false);
                string responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);                
                return new UserAuthentication(responseString, "");                
            }
            catch (Exception ex)
            {
                return new UserAuthentication(null, ex.ToString());
            }
        }
    }
}


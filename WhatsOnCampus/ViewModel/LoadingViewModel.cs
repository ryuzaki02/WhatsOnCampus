using System;
using Newtonsoft.Json;
using WhatsOnCampus.Model;
using WhatsOnCampus.Views;
using WhatsOnCampus.Controls;

namespace WhatsOnCampus.ViewModel
{
    public partial class LoadingViewModel : BaseViewModel
    {
        public LoadingViewModel()
        {
            CheckIfUserLoggedIn();
        }

        private async void CheckIfUserLoggedIn()
        {
            string userDetails = Preferences.Get(nameof(App.user), "");
            if (string.IsNullOrWhiteSpace(userDetails))
            {                                
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            else
            {
                User user = JsonConvert.DeserializeObject<User>(userDetails);
                App.user = user;
                AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
                await Shell.Current.GoToAsync($"//{nameof(FeedPage)}");
            }
        }
    }
}


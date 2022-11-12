using System;
using Microsoft.Toolkit.Mvvm.Input;
using WhatsOnCampus.MSAL;
using WhatsOnCampus.Views;

namespace WhatsOnCampus.ViewModel
{
	public partial class AppShellViewModel : BaseViewModel
	{
		[ICommand]
		async void SignOut()
		{
            _ = await PCAWrapper.Instance.SignOutAsync().ContinueWith(async (t) =>
            {
                if (Preferences.ContainsKey(nameof(App.user)))
                {
                    Preferences.Remove(nameof(App.user));
                }
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }).ConfigureAwait(false);            				
		}
	}
}


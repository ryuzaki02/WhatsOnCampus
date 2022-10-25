
using WhatsOnCampus.Auth0;
using Microsoft.Identity.Client;
using System.Diagnostics;

namespace WhatsOnCampus;

public partial class MainPage : ContentPage
{
    private readonly Auth0Client auth0Client;
    public IPublicClientApplication IdentityClient { get; set; }

    public MainPage(Auth0Client client)
	{
		InitializeComponent();
        auth0Client = client;
        //_ = GetAuthenticationToken();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        _ = GetAuthenticationToken();
        //var loginResult = await auth0Client.LoginAsync();

        //if (!loginResult.IsError)
        //{
        //    LoginView.IsVisible = false;
        //    HomeView.IsVisible = true;
        //}
        //else
        //{
        //    await DisplayAlert("Error", loginResult.ErrorDescription, "OK");
        //}
    }

    async Task<AuthenticationToken> GetAuthenticationToken()
    {
        if (IdentityClient == null)
        {
            object parentWindow = null;
#if ANDROID
            parentWindow = Platform.CurrentActivity;
#endif
            IdentityClient = PlatformService.GetIdentityClient(parentWindow);
        }

        var accounts = await IdentityClient.GetAccountsAsync();
        AuthenticationResult result = null;
        bool tryInteractiveLogin = false;

        try
        {
            result = await IdentityClient
                .AcquireTokenSilent(Constants.Scopes, accounts.FirstOrDefault())
                .ExecuteAsync();
        }
        catch (MsalUiRequiredException)
        {
            tryInteractiveLogin = true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"MSAL Silent Error: {ex.Message}");
        }

        if (tryInteractiveLogin)
        {
            try
            {
                result = await IdentityClient
                    .AcquireTokenInteractive(Constants.Scopes)
                    .ExecuteAsync()
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"MSAL Interactive Error: {ex.Message}");
            }
        }

        return new AuthenticationToken
        {
            DisplayName = result?.Account?.Username ?? "",
            ExpiresOn = result?.ExpiresOn ?? DateTimeOffset.MinValue,
            Token = result?.AccessToken ?? "",
            UserId = result?.Account?.Username ?? ""
        };
    }

}



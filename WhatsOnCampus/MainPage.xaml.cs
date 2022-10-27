
using WhatsOnCampus.Auth0;
using Microsoft.Identity.Client;
using System.Diagnostics;

namespace WhatsOnCampus;

public partial class MainPage : ContentPage
{
    // Object of Auth0 authentication
    private readonly Auth0Client auth0Client;
    // Object to manage Microsoft Authentication
    public IPublicClientApplication IdentityClient { get; set; }

    /// <summary>
    /// Constructor to the class
    /// Initialises class with Auth0Client and assign it to local variable
    /// </summary>
    /// <param name="client"></param>
    public MainPage(Auth0Client client)
	{
		InitializeComponent();
        auth0Client = client;
        //_ = GetAuthenticationToken();
    }

    /// <summary>
    /// Login button action
    /// It has currently two methods one to instantiate Auth0 login and other for Microsoft login
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        // This methods takes user to microsoft authentication
        //_ = GetAuthenticationToken();

        // This method takes user to Auth0 or google sign in
        var loginResult = await auth0Client.LoginAsync();

        // Checks if Auth0 user logged in or not
        // Updates UI accordingly
        if (!loginResult.IsError)
        {
            LoginView.IsVisible = false;
            HomeView.IsVisible = true;
        }
        else
        {
            await DisplayAlert("Error", loginResult.ErrorDescription, "OK");
        }
    }

    /// <summary>
    /// This method is to handle microsoft authentication
    /// </summary>
    /// <returns></returns>
    async Task<AuthenticationToken> GetAuthenticationToken()
    {
        // Checks if Identity client is null or not
        // if true then set parent window to current activity
        // if false then as Platform service to instantiate Identitiy Client for the parent window and navigate it 
        if (IdentityClient == null)
        {
            object parentWindow = null;
#if ANDROID
            parentWindow = Platform.CurrentActivity;
#endif
            IdentityClient = PlatformService.GetIdentityClient(parentWindow);
        }

        // Once Identity client is intialised, it then calls the server for user login and account information
        var accounts = await IdentityClient.GetAccountsAsync();
        AuthenticationResult result = null;
        bool tryInteractiveLogin = false;

        // This try-catch block checks for silent token for the user
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

        // If interactive login is true then this method executes
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

        // Return an object of Authentication token with the details of user
        // has basic details such as name, toke, user id and expiry
        return new AuthenticationToken
        {
            DisplayName = result?.Account?.Username ?? "",
            ExpiresOn = result?.ExpiresOn ?? DateTimeOffset.MinValue,
            Token = result?.AccessToken ?? "",
            UserId = result?.Account?.Username ?? ""
        };
    }

}



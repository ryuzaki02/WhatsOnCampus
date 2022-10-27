using System;
using IdentityModel.Client;
using IdentityModel.OidcClient.Browser;

namespace WhatsOnCampus.Auth0
{
    /// <summary>
    /// This is the major class which redirects user to web browser to authenticate
    /// </summary>
	public class WebBrowserAuthenticator : IdentityModel.OidcClient.Browser.IBrowser
    {
        /// <summary>
        /// Method to invoke authentication on browser aysnchronosly
        /// </summary>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            // This try catch block performs result of authenticator and return it
            try
            {
                WebAuthenticatorResult result = await WebAuthenticator.Default.AuthenticateAsync(
                    new Uri(options.StartUrl),
                    new Uri(options.EndUrl));

                // Create url with properties and end url
                var url = new RequestUrl(options.EndUrl)
                    .Create(new Parameters(result.Properties));

                // Returns Browser result object with response and success
                return new BrowserResult
                {
                    Response = url,
                    ResultType = BrowserResultType.Success
                };
            }
            catch (TaskCanceledException)
            {
                // Returns Browser result object with response and failure
                return new BrowserResult
                {
                    ResultType = BrowserResultType.UserCancel,
                    ErrorDescription = "Login canceled by the user."
                };
            }
        }
    }
}


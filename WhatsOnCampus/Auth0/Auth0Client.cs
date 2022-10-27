using System;
using IdentityModel.OidcClient;

namespace WhatsOnCampus.Auth0
{
    /// <summary>
    /// This class is responsible for Auth0 setup and authentication with options
    /// </summary>
	public class Auth0Client
	{
        private readonly OidcClient oidcClient;

        /// <summary>
        /// Constructor that initialises object with Auth0ClientOptions
        /// </summary>
        /// <param name="options"></param>
        public Auth0Client(Auth0ClientOptions options)
        {
            // Instantiate client with paramteres such as client id, scope, redirect uri etc.
            oidcClient = new OidcClient(new OidcClientOptions
            {
                Authority = $"https://{options.Domain}",
                ClientId = options.ClientId,
                Scope = options.Scope,
                RedirectUri = options.RedirectUri,
                Browser = options.Browser
            });
        }

        /// <summary>
        /// Checks the browser availability
        /// </summary>
        public IdentityModel.OidcClient.Browser.IBrowser Browser
        {
            get
            {
                return oidcClient.Options.Browser;
            }
            set
            {
                oidcClient.Options.Browser = value;
            }
        }

        /// <summary>
        /// Method to perform login task
        /// </summary>
        /// <returns></returns>
        public async Task<LoginResult> LoginAsync()
        {
            return await oidcClient.LoginAsync();
        }
    }
}


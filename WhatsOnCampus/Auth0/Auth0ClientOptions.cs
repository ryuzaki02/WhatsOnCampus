using System;
namespace WhatsOnCampus.Auth0
{
    /// <summary>
    /// Class which is responsible to hold Auth0Client options and their attributes
    /// </summary>
	public class Auth0ClientOptions
	{
        public Auth0ClientOptions()
        {
            Scope = "openid";
            RedirectUri = "myapp://callback";
            Browser = new WebBrowserAuthenticator();
        }

        public string Domain { get; set; }

        public string ClientId { get; set; }

        public string RedirectUri { get; set; }

        public string Scope { get; set; }

        public IdentityModel.OidcClient.Browser.IBrowser Browser { get; set; }
    }
}


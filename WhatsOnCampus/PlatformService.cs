using System;
using Microsoft.Identity.Client;

namespace WhatsOnCampus
{
    internal class PlatformService
    {
        /// <summary>
        /// This method is actually instantiating identity client on the parent window
        /// it initialised client application builder object with application id and redirect uri
        /// </summary>
        /// <param name="parentWindow"></param>
        /// <returns>"IPublicClientApplication"</returns>
        public static IPublicClientApplication GetIdentityClient(object parentWindow)
        {
            var clientBuilder = PublicClientApplicationBuilder
                .Create(Constants.ApplicationId)
                .WithAuthority(AzureCloudInstance.AzurePublic, "common");

#if ANDROID
            clientBuilder = clientBuilder
                .WithRedirectUri($"msal{Constants.ApplicationId}://auth")
                .WithParentActivityOrWindow(() => parentWindow);
#endif

#if WINDOWS
            clientBuilder = clientBuilder
                .WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient");
#endif

            return clientBuilder.Build();
        }
    }
}


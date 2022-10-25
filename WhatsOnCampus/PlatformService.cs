using System;
using Microsoft.Identity.Client;

namespace WhatsOnCampus
{
    internal class PlatformService
    {
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


using System;
using Android.App;
using Android.Content.PM;


namespace WhatsOnCampus;

/// <summary>
/// Class is to handle Auth0 for Android
/// Provides call back scheme and other options for the device to behave accordingly
/// </summary>
[Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
[IntentFilter(new[] { Android.Content.Intent.ActionView },
              Categories = new[] {
                Android.Content.Intent.CategoryDefault,
                Android.Content.Intent.CategoryBrowsable
              },
              DataScheme = CALLBACK_SCHEME)]
public class WebAuthenticationCallbackActivity: Microsoft.Maui.Authentication.WebAuthenticatorCallbackActivity
{
    const string CALLBACK_SCHEME = "myapp";
}
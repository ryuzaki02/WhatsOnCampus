using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace WhatsOnCampus.Platforms.Android
{
    [Activity(Exported = true)]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
        DataHost = "auth",
        DataScheme = "msalf9a5d166-2d8d-47e4-a96d-b01d1847bae9")]
    public class MsalActivity : BrowserTabActivity
    {
    }
}


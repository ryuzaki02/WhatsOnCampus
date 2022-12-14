using CommunityToolkit.Maui;
using WhatsOnCampus.Auth0;
using WhatsOnCampus.Views;
using WhatsOnCampus.ViewModel;
using UraniumUI;

namespace WhatsOnCampus;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
        builder
          .UseMauiApp<App>().ConfigureMauiHandlers(handlers =>
          {
              handlers.AddUraniumUIHandlers(); // 👈 This line should be added.
          })
          .ConfigureFonts(fonts =>
          {
              fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
              fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
          }).UseMauiCommunityToolkit();

#if DEBUG
        //builder.Logging.AddDebug();
#endif

        //Views
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<ProfilePage>();
        builder.Services.AddSingleton<FeedPage>();
        builder.Services.AddSingleton<LoadingPage>();


        //ViewModels
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<ProfileViewModel>();
        builder.Services.AddSingleton<FeedViewModel>();
        builder.Services.AddSingleton<AppShellViewModel>();
        builder.Services.AddSingleton<LoadingViewModel>();

        builder.Services.AddSingleton(new Auth0Client(new()
        {
            Domain = "dev-0l7jz8pmfwfo4hmd.us.auth0.com",
            ClientId = "vjW3j2wbm75RuhKj6fnHvrEykY2TmO9K",
            Scope = "openid profile",
            RedirectUri = "myapp://callback"
        }));

        return builder.Build();
	}
}


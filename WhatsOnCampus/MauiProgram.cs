using WhatsOnCampus.Auth0;

namespace WhatsOnCampus;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
        builder
          .UseMauiApp<App>()
          .ConfigureFonts(fonts =>
          {
              fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
              fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
          });

#if DEBUG
        //builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<MainPage>();

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


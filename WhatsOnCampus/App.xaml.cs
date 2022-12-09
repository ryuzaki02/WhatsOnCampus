using WhatsOnCampus.Views;
using WhatsOnCampus.Model;
using WhatsOnCampus.Services;

namespace WhatsOnCampus;

public partial class App : Application
{
	public static User user;

	public App()
	{
        DependencyService.Register<FeedDataStoreAPI>();

        DependencyService.Register<WebClientService>();

        DependencyService.Register<Services.UserDatastore>();

        InitializeComponent();

		MainPage = new AppShell();
	}
}


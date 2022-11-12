using WhatsOnCampus.Views;
using WhatsOnCampus.Model;

namespace WhatsOnCampus;

public partial class App : Application
{
	public static User user;

	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}


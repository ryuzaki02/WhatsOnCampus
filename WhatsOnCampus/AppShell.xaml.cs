using WhatsOnCampus.Views;
using WhatsOnCampus.ViewModel;
namespace WhatsOnCampus;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		this.BindingContext = new AppShellViewModel();
		Routing.RegisterRoute(nameof(FeedPage), typeof(FeedPage));
    }
}


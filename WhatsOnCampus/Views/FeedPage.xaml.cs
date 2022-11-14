using WhatsOnCampus.ViewModel;
namespace WhatsOnCampus.Views;

public partial class FeedPage : ContentPage
{
	public FeedPage(FeedViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}

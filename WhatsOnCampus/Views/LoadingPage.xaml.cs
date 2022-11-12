using WhatsOnCampus.ViewModel;

namespace WhatsOnCampus.Views;

public partial class LoadingPage : ContentPage
{
	public LoadingPage(LoadingViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}

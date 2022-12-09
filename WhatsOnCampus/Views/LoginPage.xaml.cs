using WhatsOnCampus.ViewModel;
using WhatsOnCampus.Views;

namespace WhatsOnCampus.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}

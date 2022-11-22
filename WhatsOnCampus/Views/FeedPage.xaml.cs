using WhatsOnCampus.ViewModel;
namespace WhatsOnCampus.Views;

public partial class FeedPage : ContentPage
{
	private FeedViewModel localViewModel;

	public FeedPage(FeedViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
		localViewModel = viewModel;
	}

    void OnSearchTextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        SearchBar searchBar = (SearchBar)sender;
		localViewModel.onSearchBarTextChangedAsync(searchBar.Text);
    }
}

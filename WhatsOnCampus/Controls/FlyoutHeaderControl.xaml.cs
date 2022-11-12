namespace WhatsOnCampus.Controls;

public partial class FlyoutHeaderControl : StackLayout
{
	public FlyoutHeaderControl()
	{
		InitializeComponent();

		if (App.user != null)
		{
			userName.Text = App.user.displayName;
		}
	}
}

using Java.Util.Streams;
using WhatsOnCampus.ViewModel;
using Firebase.Storage;

namespace WhatsOnCampus.Views;

public partial class ProfilePage : ContentPage
{
    ImageSource imageStream;
    private string firebaseStorage = "whatsoncampus-77c50.appspot.com";
    public ProfilePage(ProfileViewModel viewModel){
		InitializeComponent();
		BindingContext = viewModel;
	}

    async void editProfilePic(System.Object sender, System.EventArgs e)
    {
        string action = await DisplayActionSheet("Open With", "Cancel", null, "Camera", "Files");
        if (action == "Camera")
        {    // Options for choosing image
            var result = await MediaPicker.CapturePhotoAsync();
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                imageStream = ImageSource.FromStream(() => stream);
                ProfilePic.Source = imageStream;

                try
                {
                    var downloadLink = await new FirebaseStorage(firebaseStorage).Child("ProfilePic/" + result.FileName).PutAsync(await result.OpenReadAsync());

                    Console.WriteLine(downloadLink);
                    //viewModel.DownloadImage = downloadLink;


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


        }
        else if (action == "Files")
        { // choose the image
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Pick Image please",
                FileTypes = FilePickerFileType.Images

            });

            if (result == null) return;

            if (result != null)
            {   // finding the source of the image
                var stream = await result.OpenReadAsync();
                imageStream = ImageSource.FromStream(() => stream);
                ProfilePic.Source = imageStream;
                try
                {   // store the image data in firebase
                    var downloadLink = await new FirebaseStorage(firebaseStorage).Child("ProfilePic/" + result.FileName).PutAsync(await result.OpenReadAsync());
                    Console.WriteLine(downloadLink);
                } // if the exception found
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

    }
}

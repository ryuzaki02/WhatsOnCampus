
using WhatsOnCampus.Auth0;
using Microsoft.Identity.Client;
using System.Diagnostics;
using WhatsOnCampus.MSAL;
using Google.Cloud.Firestore;
using static Google.Cloud.Firestore.V1.StructuredQuery.Types;
using Google.Cloud.Firestore.V1;
using WhatsOnCampus.ViewModel;

namespace WhatsOnCampus.Views;

public partial class MainPage : ContentPage
{
    // Object of Auth0 authentication
    private readonly Auth0Client auth0Client;
    // Object to manage Microsoft Authentication
    public IPublicClientApplication IdentityClient { get; set; }
    

    /// <summary>
    /// Constructor to the class
    /// Initialises class with Auth0Client and assign it to local variable
    /// </summary>
    /// <param name="client"></param>
    public MainPage(Auth0Client client)
    {
        InitializeComponent();
        auth0Client = client;
        
    }

    /// <summary>
    /// Login button action
    /// It has currently two methods one to instantiate Auth0 login and other for Microsoft login
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("profilePage");
        //UserAuthentication userAuthentication = await viewModel.AuthenticateMicrosoft();
    }

    public async Task<FirestoreDb> initFirestore()
    {//whatsoncampus-b5a48-firebase-adminsdk-3qtvb-ee24678c31.json
        try
        {
            var stream = await FileSystem.OpenAppPackageFileAsync("whatsoncampus-b5a48-firebase-adminsdk-3qtvb-ee24678c31.json");
            var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();

            FirestoreClientBuilder fbc = new FirestoreClientBuilder { JsonCredentials = contents };
            return FirestoreDb.Create("whatsoncampus-b5a48", fbc.Build());
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async void ConnectToFireStore()
    {
        //whatsoncampus-b5a48-firebase-adminsdk-3qtvb-ee24678c31.json
        //google-services.json
        var localPath = Path.Combine(FileSystem.CacheDirectory, "GoogleServices.json");

        using var json = await FileSystem.OpenAppPackageFileAsync("GoogleServices.json");
        using var dest = File.Create(localPath);
        await json.CopyToAsync(dest);

        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", localPath);
        dest.Close();

        //FirestoreDb db = await initFirestore();



        FirestoreDb db = FirestoreDb.Create("whatsoncampus-b5a48");

        // Create a document with a random ID in the "users" collection.
        CollectionReference collection = db.Collection("users");
        DocumentReference document = await collection.AddAsync(new { Name = new { First = "Ada", Last = "Lovelace" }, Born = 1815 });

        // A DocumentReference doesn't contain the data - it's just a path.
        // Let's fetch the current document.
        DocumentSnapshot snapshot = await document.GetSnapshotAsync();

        // We can access individual fields by dot-separated path
        Console.WriteLine(snapshot.GetValue<string>("Name.First"));
        Console.WriteLine(snapshot.GetValue<string>("Name.Last"));
        Console.WriteLine(snapshot.GetValue<int>("Born"));
    }

    private async Task ShowMessage(string title, string message)
    {
        _ = this.Dispatcher.Dispatch(async () =>
        {
            await DisplayAlert(title, message, "OK").ConfigureAwait(false);
        });
    }

}
using System;
using WhatsOnCampus.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Microsoft.Toolkit.Mvvm.Input;
using Google.Api;
using Newtonsoft.Json;

namespace WhatsOnCampus.ViewModel
{
    public partial class ProfileViewModel : BaseViewModel
    {
        Services.UserDatastore service = DependencyService.Get<Services.UserDatastore>();
        public ObservableRangeCollection<Profile> Profiles { get; set; }
        public string displayName { get; set; }

        public ProfileViewModel()
        {
            Profiles = new ObservableRangeCollection<Profile>();
            User user = App.user;

            Profile email = new Profile();
            email.title = "Email Address";
            email.subtitle = user.mail;

            Profile phone = new Profile();
            phone.title = "Phone";
            phone.subtitle = user.mobilePhone == null ? "Not Available" : user.mobilePhone.ToString();

            Profile language = new Profile();
            language.title = "Preferred Language";
            language.subtitle = user.preferredLanguage == null ? "Not Available" : user.preferredLanguage.ToString();

            displayName = user.displayName;

            Profiles.Add(email);
            Profiles.Add(phone);
            Profiles.Add(language);
        }

        [ICommand]
        async void Save()
        {
            Console.Write("save");
            var userInfo = JsonConvert.DeserializeObject<User>(Preferences.Get("User", ""));
            var newUser = new User
            {
                FirstName = userInfo.FirstName,
                displayName = userInfo.displayName,
                mail = userInfo.mail,
                id = userInfo.id,
                givenName = userInfo.givenName,
                userPrincipalName = userInfo.userPrincipalName,
                surname = userInfo.surname
            };

            await service.AddUpdateAsync(newUser);
        }
    }
}


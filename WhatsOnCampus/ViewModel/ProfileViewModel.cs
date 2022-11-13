using System;
using WhatsOnCampus.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace WhatsOnCampus.ViewModel
{
    public partial class ProfileViewModel : BaseViewModel
    {
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
    }
}


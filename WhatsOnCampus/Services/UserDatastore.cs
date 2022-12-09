using System;
using WhatsOnCampus.Model;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;

namespace WhatsOnCampus.Services
{
    public class UserDatastore : IFirebase
    {
        private readonly FirebaseClient _client = new FirebaseClient(Settings.Firebase.URL,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(Settings.Firebase.Secret)
                });

        public UserDatastore()
        {
        }

        public async Task<String>AddUpdateAsync(User user)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(user.Key))
                {
                   await _client.Child("Profile").Child(user.Key).PutAsync(user);
                    return user.Key;
                }
                else
                {
                    var response = await _client.Child("Profile").PostAsync(user);
                    if (response.Key != null)
                        return response.Key;    
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return null;
        }

        public Task<bool> DeleteAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<bool> IFirebase.AddUpdateAsync(User model)
        {
            throw new NotImplementedException();
        }
    }
}


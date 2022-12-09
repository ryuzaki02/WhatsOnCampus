using System;
using WhatsOnCampus.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
#if ANDROID
using static Android.Provider.DocumentsContract;
#endif
namespace WhatsOnCampus.Services
{
    public class FeedDataStoreAPI : IFeedDataStore
    {
        public async Task<TwitterRoot> GetTweets(string searchQuery, string nextTweetPageFeedId)
        {
            var service = DependencyService.Get<IWebClientService>();
            string url = $"https://api.twitter.com/2/tweets/search/recent?tweet.fields=created_at,attachments&expansions=author_id&user.fields=profile_image_url&query={searchQuery}" + (nextTweetPageFeedId.Length > 0 ? $"&next_token={nextTweetPageFeedId}" : "");
            var json = await service.GetTwitterFeed(url);
            var tweet = JsonConvert.DeserializeObject<TwitterRoot>(json);
            System.Diagnostics.Debug.WriteLine(url);
            System.Diagnostics.Debug.WriteLine(tweet.data.Count);
            System.Diagnostics.Debug.WriteLine(tweet.meta.next_token);            
            return tweet;
        }

        public static class UserBuilder
        {

        }
    }
}


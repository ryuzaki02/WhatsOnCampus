using System;
using WhatsOnCampus.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using static Android.Provider.DocumentsContract;

namespace WhatsOnCampus.Services
{
    public class FeedDataStoreAPI : IFeedDataStore
    {
        public async Task<TwitterRoot> GetTweets()
        {
            var service = DependencyService.Get<IWebClientService>();
            var json = await service.GetTwitterFeed("https://api.twitter.com/2/tweets/search/recent?tweet.fields=created_at,attachments&expansions=author_id&user.fields=profile_image_url&query=cambriancollege");
        
            var tweet = JsonConvert.DeserializeObject<TwitterRoot>(json);

            return tweet;
        }

        public static class UserBuilder
        {

        }
    }
}


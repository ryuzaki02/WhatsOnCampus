using System;
namespace WhatsOnCampus.Model
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Attachments
    {
        public List<string> media_keys { get; set; }
    }

    public class Datum
    {
        public string text { get; set; }
        public string author_id { get; set; }
        public Attachments attachments { get; set; }
        public List<string> edit_history_tweet_ids { get; set; }
        public string id { get; set; }
        public DateTime created_at { get; set; }
    }

    public class Includes
    {
        public List<TwitterUser> users { get; set; }
    }

    public class Meta
    {
        public string newest_id { get; set; }
        public string oldest_id { get; set; }
        public int result_count { get; set; }
        public string next_token { get; set; }
    }

    public class TwitterRoot
    {
        public List<Datum> data { get; set; }
        public Includes includes { get; set; }
        public Meta meta { get; set; }
    }

    public class TwitterUser
    {
        public string id { get; set; }
        public string profile_image_url { get; set; }
        public string name { get; set; }
        public string username { get; set; }
    }
}


using System;
using WhatsOnCampus.Model;

namespace WhatsOnCampus.Services
{
	public interface IFeedDataStore
	{
        Task<TwitterRoot> GetTweets(string searchQuery);
    }
}


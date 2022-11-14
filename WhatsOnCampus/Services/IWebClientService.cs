using System;
namespace WhatsOnCampus.Services
{
    public interface IWebClientService
    {
        Task<string> GetTwitterFeed(string uri);
    }
}


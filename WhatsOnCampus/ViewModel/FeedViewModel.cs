using System;
using Google.Apis.Util.Store;
using WhatsOnCampus.Model;
using WhatsOnCampus.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace WhatsOnCampus.ViewModel
{
	public partial class FeedViewModel : BaseViewModel
	{        
        public IFeedDataStore DataStore => DependencyService.Get<IFeedDataStore>();
        public ObservableRangeCollection<FeedModel> feedModels { get; set; }
        public AsyncCommand RefreshCommand { get; }

        public FeedViewModel()
        {
            feedModels = new ObservableRangeCollection<FeedModel>();
            RefreshCommand = new AsyncCommand(getTweets);
            getTweets();
        }

        public async Task getTweets()
        {
            feedModels.Clear();
            TwitterRoot root = await DataStore.GetTweets();
            for (int i = 0; i < root.data.Count; i++)
            {
                FeedModel feedModel = new FeedModel();
                TwitterUser user = GetTwitterUser(root.data[i].author_id, root);
                feedModel.name = user.username;
                feedModel.dateTime = root.data[i].created_at.ToString();
                feedModel.postContent = root.data[i].text;
                feedModel.profileImageUrl = user.profile_image_url;
                feedModels.Add(feedModel);
            }
            Console.WriteLine(feedModels);
            IsBusy = false;
        }

        private TwitterUser GetTwitterUser(string authorId, TwitterRoot root)
        {
            foreach (TwitterUser user in root.includes.users)
            {
                if (user.id.Equals(authorId))
                {
                    return user;
                }
            }
            return null;
        }
    }
}


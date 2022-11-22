using System;
using Google.Apis.Util.Store;
using WhatsOnCampus.Model;
using WhatsOnCampus.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Microsoft.Toolkit.Mvvm.Input;
using Android.App.AppSearch;

namespace WhatsOnCampus.ViewModel
{
	public partial class FeedViewModel : BaseViewModel
	{        
        public IFeedDataStore DataStore => DependencyService.Get<IFeedDataStore>();
        public ObservableRangeCollection<FeedModel> feedModels { get; set; }
        public AsyncCommand RefreshCommand { get; }
        private string searchQuery = "cambriancollege";

        public FeedViewModel()
        {
            feedModels = new ObservableRangeCollection<FeedModel>();
            RefreshCommand = new AsyncCommand(getTweets);
            getTweets();
        }

        public async Task getTweets()
        {
            feedModels.Clear();
            TwitterRoot root = await DataStore.GetTweets(searchQuery);
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

        private CancellationTokenSource _throttleCts = new CancellationTokenSource();

        public async Task onSearchBarTextChangedAsync(String text)
        {
            Console.WriteLine(text);
            searchQuery = text.Length > 0 ? text : "cambriancollege";
            try
            {
                Interlocked.Exchange(ref _throttleCts, new CancellationTokenSource()).Cancel();

                //NOTE THE 500 HERE - WHICH IS THE TIME TO WAIT
                await Task.Delay(TimeSpan.FromMilliseconds(500), _throttleCts.Token)

                    //NOTICE THE "ACTUAL" SEARCH METHOD HERE
                    .ContinueWith(async task => await getTweets(),
                        CancellationToken.None,
                        TaskContinuationOptions.OnlyOnRanToCompletion,
                        TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch
            {
                //Ignore any Threading errors
            }
        }
    }
}


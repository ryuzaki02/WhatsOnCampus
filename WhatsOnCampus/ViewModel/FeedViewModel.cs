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
        public bool IsLoading { get; set; }
        public bool IsRefresh { get; set; }
        private string searchQuery = "";
        private string nextTweetPageFeedId = "";

        public FeedViewModel()
        {
            feedModels = new ObservableRangeCollection<FeedModel>();                        
            Refresh();
        }

        [ICommand]
        public void Refresh()
        {
            searchQuery = "cambriancollege";
            IsRefresh = true;
            getTweets();
        }

        public async Task getTweets()
        {
            if (IsRefresh == true) feedModels.Clear();

            TwitterRoot root = await DataStore.GetTweets(searchQuery, nextTweetPageFeedId);
            for (int i = 0; i < root.data.Count; i++)
            {
                FeedModel feedModel = new FeedModel();
                TwitterUser user = GetTwitterUser(root.data[i].author_id, root);
                nextTweetPageFeedId = root.meta.next_token;
                feedModel.name = user.username;
                feedModel.dateTime = root.data[i].created_at.ToString();
                feedModel.postContent = root.data[i].text;
                feedModel.profileImageUrl = user.profile_image_url;
                feedModels.Add(feedModel);
            }
            IsBusy = false;
            IsLoading = false;
            IsRefresh = false;
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

        [ICommand]
        public async void LoadMoreData()
        {
            await Task.Delay(1000);
            if (IsLoading == true) return;            
            if (feedModels.Count > 0)
            {
                IsLoading = true;
                IsRefresh = false;                
                getTweets();
            }
        }
    }
}


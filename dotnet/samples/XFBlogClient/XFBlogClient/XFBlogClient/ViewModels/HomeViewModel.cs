using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Bogus;
using MvvmHelpers;
using Xamarin.Essentials;
using Xamarin.Forms;
using XFBlogClient.Models;
using XFBlogClient.Services;
using XFBlogClient.Views;

namespace XFBlogClient.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ObservableRangeCollection<BlogPost> BlogPosts { get; set; }

        public Command LoginCommand { get; set; }
        private string _avatarUrl;
        public string AvatarUrl
        {
            get => _avatarUrl;
            set => SetProperty(ref _avatarUrl, value);
        }

        private BlogPost _selectedBlogPost;
        public BlogPost SelectedBlogPost
        {
            get => _selectedBlogPost;
            set => SetProperty(ref _selectedBlogPost, value);
        }

        public Command ShowBlogPost { get; set; }

        public HomeViewModel()
        {
            LoginCommand = new Command(async () => OnLogin());
            ShowBlogPost = new Command(async (post) => await OnShowBlogPost());
            Title = "Home";

            AvatarUrl = new Faker().Internet.Avatar();
            BlogPosts = new ObservableRangeCollection<BlogPost>();
            
        }

        public async Task InitAsync()
        {
            var dataStore = DependencyService.Get<IDataStore<BlogPost>>();

            var blogPosts = await dataStore.GetItemsAsync();
            BlogPosts.ReplaceRange(blogPosts);
        }

        private async Task OnLogin()
        {
            var dataService = DependencyService.Get<IDataStore<BlogPost>>();
            await dataService.Login();

            var blogPosts = await dataService.GetItemsAsync();
            await AddPosts(dataService);
            BlogPosts.ReplaceRange(blogPosts);
        }

        private async Task AddPosts(IDataStore<BlogPost> dataService)
        {
            await dataService.AddItemAsync(new BlogPost()
            {
                Title = "Your guide to Azure services for apps built with Xamarin",
                Data = "When talking about app development today, the cloud is almost always part of the conversation. While many developers have an idea of the benefits that cloud can offer them – scalability, ready-to-use functionality, and security, to name a few – it's sometimes hard to figure out where to start for the specific scenario you have in mind. Luckily, our mobile developer tools docs team has you covered! Today, we're happy to announce the availability of the 'Mobile apps using Xamarin + Azure' poster. This poster serves as your one-stop guide to the most relevant cloud services that Azure has to offer to you as a mobile developer with Visual Studio and Xamarin. We're excited to hear your feedback on the poster and how we can make it even better for you to get the most out of Azure services for your mobile apps built with Xamarin. Leave us a comment below with what you think and happy coding!",
                ImageUrl = "https://devblogs.microsoft.com/visualstudio/wp-content/uploads/sites/4/2018/02/poster.png"
            });
            
        }
        private async Task OnShowBlogPost()
        {
            await Shell.Current.GoToAsync($"{nameof(BlogDetailPage)}?{nameof(BlogDetailViewModel.ItemId)}={SelectedBlogPost.Id}");
        }
    }
}
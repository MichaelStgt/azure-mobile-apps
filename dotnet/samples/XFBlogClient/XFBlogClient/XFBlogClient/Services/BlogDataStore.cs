using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Mobile.Client;
using Azure.Mobile.Client.Table;
using Bogus;
using Xamarin.Forms;
using XFBlogClient.Models;

namespace XFBlogClient.Services
{
    public class BlogDataStore : IDataStore<BlogPost>
    {
        private List<BlogPost> _blogPosts;
        private readonly MobileDataClient _client;
        private MobileDataTable<BlogPost> _blogTable;

        public BlogDataStore()
        {
            var authService = DependencyService.Get<IAuthenticationService>();
            
            _client = new MobileDataClient(
                new Uri("https://blogserver-zumo-next.azurewebsites.net"), 
                authService.Credential);
        }

        private void LoadBlogPosts()
        {
            _blogTable = _client.GetTable<BlogPost>();
            _blogPosts = _blogTable.GetItems().ToList();

            AddFakeAuthorData();
        }

        public async Task<bool> AddItemAsync(BlogPost blogPost)
        {
            await _blogTable.InsertItemAsync(blogPost);

            _blogPosts.Add(blogPost);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(BlogPost blogPost)
        {
            await _blogTable.ReplaceItemAsync(blogPost);

            var oldItem = _blogPosts.FirstOrDefault(x => x.Id == blogPost.Id);
            _blogPosts.Remove(oldItem);
            _blogPosts.Add(blogPost);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = _blogPosts.FirstOrDefault(x => x.Id == id);
            _blogPosts.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<BlogPost> GetItemAsync(string id)
        {
            return await Task.FromResult(_blogPosts.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<BlogPost>> GetItemsAsync(bool forceRefresh = false)
        {
            if (_client is null)
            {
                var authService = DependencyService.Get<IAuthenticationService>();

                await authService.LoginAsync();
            }

            if (_blogPosts is null || !_blogPosts.Any() || forceRefresh)
            {
                LoadBlogPosts();
            }

            return await Task.FromResult(_blogPosts);
        }

        private void AddFakeAuthorData()
        {
            var faker = new Faker();
            foreach (var blogPost in _blogPosts)
            {
                blogPost.AuthorName = faker.Name.FullName();
                blogPost.AuthorAvatarUrl = faker.Internet.Avatar();
            }
        }

    }
}
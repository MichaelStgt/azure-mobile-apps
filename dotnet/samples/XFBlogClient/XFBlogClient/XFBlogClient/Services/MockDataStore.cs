using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Mobile.Client;
using Azure.Mobile.Client.Auth;
using Bogus;
using Microsoft.Identity.Client;
using XFBlogClient.Models;

namespace XFBlogClient.Services
{
    public class MockDataStore : IDataStore<BlogPost>
    {
        private List<BlogPost> _blogPosts;
        private MobileDataClient client;
        public MockDataStore()
        {
            //var testBlogPosts = new Faker<BlogPost>()
            //    .RuleFor(x => x.Id, i => Guid.NewGuid().ToString())
            //    .RuleFor(x => x.Title, t => t.Lorem.Sentence())
            //    .RuleFor(x => x.Data, d => d.Lorem.Paragraphs(4))
            //    .RuleFor(x => x.CommentCount, c => c.Random.Number(0, 100))
            //    .RuleFor(x => x.ShowComments, s => s.Random.Bool())
            //    .RuleFor(x => x.AuthorAvatarUrl, a => a.Internet.Avatar())
            //    .RuleFor(x => x.AuthorName, a => a.Name.FullName())
            //    .RuleFor(x => x.PostedAt, p => p.Date.Past())
            //    .RuleFor(x => x.IsBookmarked, i => i.Random.Bool())
            //    .RuleFor(x => x.ImageUrl, i => i.Image.PicsumUrl(blur:true));

            //_blogPosts = testBlogPosts.Generate(20);
        }

        public async Task Login()
        {
            if (App.AuthenticationClient is null)
            {
                throw new NullReferenceException("AuthenticationClient is null");
            }

            //foreach (var account in await App.AuthenticationClient.GetAccountsAsync())
            //{
            //    await App.AuthenticationClient.RemoveAsync(account);
            //}

            var accounts = await App.AuthenticationClient.GetAccountsAsync();
            AuthenticationResult result;
            try
            {
                result = await App.AuthenticationClient.AcquireTokenSilent(Constants.Scopes, accounts.FirstOrDefault()).ExecuteAsync();
            }
            catch (MsalUiRequiredException ex)
            {
                try
                {
                    result = await App.AuthenticationClient.AcquireTokenInteractive(Constants.Scopes).ExecuteAsync();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            var credential = new PreauthorizedTokenCredential(result.AccessToken);
            client = new MobileDataClient(new Uri("https://blogserver-zumo-next.azurewebsites.net"), credential);
        }

        private void LoadBlogPosts()
        {
            var table = client.GetTable<BlogPost>();
            try
            {
                _blogPosts = table.GetItems().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> AddItemAsync(BlogPost blogPost)
        {
            var table = client.GetTable<BlogPost>();
            try
            {
                await table.InsertItemAsync(blogPost);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            //_blogPosts.Add(blogPost);
            
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(BlogPost blogPost)
        {
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
            if (client is null)
            {
                await Login();
            }

            if (_blogPosts is null || !_blogPosts.Any())
            {
                LoadBlogPosts();
            }

            return await Task.FromResult(_blogPosts);
        }
    }
}
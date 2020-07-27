﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using XFBlogClient.Models;

namespace XFBlogClient.Services
{
    public class MockDataStore : IDataStore<BlogPost>
    {
        readonly List<BlogPost> _blogPosts;

        public MockDataStore()
        {
            var testBlogPosts = new Faker<BlogPost>()
                .RuleFor(x => x.Id, i => Guid.NewGuid().ToString())
                .RuleFor(x => x.Title, t => t.Lorem.Sentence())
                .RuleFor(x => x.Data, d => d.Lorem.Paragraphs(4))
                .RuleFor(x => x.CommentCount, c => c.Random.Number(0, 100))
                .RuleFor(x => x.ShowComments, s => s.Random.Bool())
                .RuleFor(x => x.AuthorAvatarUrl, a => a.Internet.Avatar())
                .RuleFor(x => x.AuthorName, a => a.Name.FullName())
                .RuleFor(x => x.PostedAt, p => p.Date.Past())
                .RuleFor(x => x.ImageUrl, i => i.Image.PicsumUrl());

            _blogPosts = testBlogPosts.Generate(20);
        }

        public async Task<bool> AddItemAsync(BlogPost blogPost)
        {
            _blogPosts.Add(blogPost);

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
            return await Task.FromResult(_blogPosts);
        }
    }
}
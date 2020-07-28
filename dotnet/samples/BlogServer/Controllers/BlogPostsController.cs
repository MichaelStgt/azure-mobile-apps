﻿using Azure.Mobile.Server;
using Azure.Mobile.Server.Entity;
using BlogServer.Database;
using BlogServer.DataObjects;
using BlogServer.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogServer.Controllers
{
    [Route("tables/blog_posts")]
    [ApiController]
    public class BlogPostsController : TableController<BlogPost>
    {
        private readonly IUserService _userService;

        public BlogPostsController(BlogDbContext context, IUserService userService)
        {
            TableRepository = new EntityTableRepository<BlogPost>(context);
            _userService = userService;
        }

        public override async Task<BlogPost> PrepareItemForStoreAsync(BlogPost item)
        {
            var user = await _userService.GetOrPopulateUserAsync();
            item.OwnerId = user.Id;
            item.PostedAt = DateTimeOffset.Now;
            return item;
        }

        public override bool IsAuthorized(TableOperation operation, BlogPost item)
        {
            var userId = _userService.GetUserId();

            if (operation == TableOperation.Create && userId is null)
            {
                return false;
            }

            if((operation == TableOperation.Replace || operation == TableOperation.Delete || operation == TableOperation.Patch) && item.OwnerId != userId)
            {
                return false;
            }

            return true;
        }

    }
}

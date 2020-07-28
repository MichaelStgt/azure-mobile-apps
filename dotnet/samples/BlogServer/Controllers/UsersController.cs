﻿using Azure.Mobile.Server;
using Azure.Mobile.Server.Entity;
using BlogServer.Database;
using BlogServer.DataObjects;
using BlogServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using System.Threading.Tasks;

namespace BlogServer.Controllers
{
    [Route("tables/users")]
    [ApiController]
    public class UsersController : TableController<User>
    {
        private readonly IUserService _userService;

        public UsersController(BlogDbContext context, IUserService userService)
        {
            TableRepository = new EntityTableRepository<User>(context);
            _userService = userService;
        }

        public override bool IsAuthorized(TableOperation operation, User item)
        {
            var userId = _userService.GetUserId();

            if (operation == TableOperation.Create && userId is null)
            {
                return false;
            }

            if ((operation == TableOperation.Replace || operation == TableOperation.Delete || operation == TableOperation.Patch) && item.Id != userId)
            {
                return false;
            }

            return true;
        }

        public override async Task<User> PrepareItemForStoreAsync(User item)
        {
            // Using Object Id as UserId since this ID uniquely identifies the user across applications.
            // Two different applications signing in the same user will receive the same value in the oid claim.
            // GetNameIdentifierId() returns the sub claim that it is unique to a particular application ID. 
            // Therefore, if a single user signs into two different apps using two different client IDs, 
            // those apps will receive two different values for the subject claim.
            item.Id = this.User.GetObjectId();
            return item;
        }
    }
}

using Azure.Mobile.Server;
using Azure.Mobile.Server.Entity;
using BlogServer.Database;
using BlogServer.DataObjects;
using BlogServer.Services;
using Microsoft.AspNetCore.Mvc;

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

            if (operation == TableOperation.Create)
            {
                return false;
            }

            if ((operation == TableOperation.Replace || operation == TableOperation.Delete || operation == TableOperation.Patch) && item.Id != userId)
            {
                return false;
            }

            return true;
        }

    }
}

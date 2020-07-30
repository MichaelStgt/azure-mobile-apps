using Azure.Mobile.Server;
using Azure.Mobile.Server.Entity;
using BlogServer.Database;
using BlogServer.DataObjects;
using BlogServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BlogServer.Controllers
{
    [Route("tables/bookmarks")]
    [ApiController]
    public class BookmarksController : TableController<Bookmark>
    {
        private readonly IUserService _userService;

        public BookmarksController(BlogDbContext context, IUserService userService)
        {
            TableRepository = new EntityTableRepository<Bookmark>(context);
            _userService = userService;
            TableControllerOptions = new TableControllerOptions<Bookmark>
            {
                DataView = bookmark => bookmark.UserId == _userService.GetUserId()
            };
        }

        public override async ValueTask<int> ValidateOperationAsync(TableOperation operation, Bookmark item, CancellationToken cancellationToken = default)
        {
            var user = await _userService.GetOrPopulateUserAsync(cancellationToken);

            if (user == null || (operation == TableOperation.Delete || operation == TableOperation.Replace || operation == TableOperation.Patch) && item.UserId != user.Id)
            {
                return StatusCodes.Status401Unauthorized;
            }

            if (operation == TableOperation.Create)
            {
                var existingItem = await TableRepository
                                        .AsQueryable()
                                        .FirstOrDefaultAsync(bookmark => bookmark.PostId == item.PostId && bookmark.UserId == user.Id);

                if (existingItem != null)
                {
                    return StatusCodes.Status400BadRequest;
                }
            }

            return StatusCodes.Status200OK;
        }

        public override async Task<Bookmark> PrepareItemForStoreAsync(Bookmark item)
        {
            var user = await _userService.GetOrPopulateUserAsync();
            item.UserId = user.Id;
            return item;
        }
    }
}

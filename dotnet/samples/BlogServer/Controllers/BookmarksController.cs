using Azure.Mobile.Server;
using Azure.Mobile.Server.Entity;
using BlogServer.Database;
using BlogServer.DataObjects;
using BlogServer.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        }
    }
}

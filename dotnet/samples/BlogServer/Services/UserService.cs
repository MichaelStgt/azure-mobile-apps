using BlogServer.Database;
using BlogServer.DataObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Web;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlogServer.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly BlogDbContext _dbContext;
        private readonly ITokenAcquisition _tokenAcquisition;

        public UserService(IHttpContextAccessor contextAccessor, BlogDbContext dbContext, ITokenAcquisition tokenAcquisition)
        {
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _tokenAcquisition = tokenAcquisition;
        }

        public string GetUserId()
        {
            // Using Object Id as UserId since this ID uniquely identifies the user across applications.
            // Two different applications signing in the same user will receive the same value in the oid claim.
            // GetNameIdentifierId() returns the sub claim that it is unique to a particular application ID. 
            // Therefore, if a single user signs into two different apps using two different client IDs, 
            // those apps will receive two different values for the subject claim.
            return _contextAccessor?.HttpContext.User.GetObjectId();
        }

        public string GetName()
        {
            return _contextAccessor?.HttpContext.User.FindFirst("name")?.Value;
        }

        public string GetPreferredUsername()
        {
            return _contextAccessor?.HttpContext.User.GetDisplayName();
        }

        public async Task<User> GetOrPopulateUserAsync(CancellationToken cancellationToken = default)
        {
            var userId = GetUserId();
            var user = await _dbContext.Users.FindAsync(userId);

            // User doesn't exist yet on our database so we will populate its data
            // TODO: User profile image from Graph (maybe getting more data from the user profile)
            if(user == null && !string.IsNullOrEmpty(userId))
            {
                //string[] scopes = { "user.read" };
                //var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(scopes);

                user = _dbContext.Users.Add(new User
                {
                    Id = userId,
                    Username = GetName() ?? GetPreferredUsername()
                }).Entity;
            }

            return user;
        }
    }
}

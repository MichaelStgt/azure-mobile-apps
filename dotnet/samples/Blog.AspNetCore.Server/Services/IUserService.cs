using Blog.AspNetCore.Server.DataObjects;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.AspNetCore.Server.Services
{
    public interface IUserService
    {
        string GetName();
        Task<User> GetOrPopulateUserAsync(CancellationToken cancellationToken = default);
        string GetPreferredUsername();
        string GetUserId();
    }
}

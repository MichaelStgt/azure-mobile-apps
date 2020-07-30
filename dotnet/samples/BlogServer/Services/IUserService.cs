using BlogServer.DataObjects;
using System.Threading;
using System.Threading.Tasks;

namespace BlogServer.Services
{
    public interface IUserService
    {
        string GetName();
        Task<User> GetOrPopulateUserAsync(CancellationToken cancellationToken = default);
        string GetPreferredUsername();
        string GetUserId();
    }
}

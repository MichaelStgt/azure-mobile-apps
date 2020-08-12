using System.Threading.Tasks;
using Azure.Core;

namespace XFBlogClient.Services
{
    public interface IAuthenticationService
    {
        Task LoginAsync();
        TokenCredential Credential { get; }
    }
}
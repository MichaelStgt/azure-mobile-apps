using System;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Mobile.Client.Auth;
using Microsoft.Identity.Client;
using XFBlogClient.Models;

namespace XFBlogClient.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task LoginAsync()
        {
            if (App.AuthenticationClient is null)
            {
                throw new NullReferenceException("AuthenticationClient is null");
            }

            var accounts = await App.AuthenticationClient.GetAccountsAsync();
            AuthenticationResult result;

            try
            {
                result = await App.AuthenticationClient.AcquireTokenSilent(Constants.Scopes, accounts.FirstOrDefault()).ExecuteAsync();
            }
            catch (MsalUiRequiredException ex)
            {
                try
                {
                    result = await App.AuthenticationClient.AcquireTokenInteractive(Constants.Scopes).ExecuteAsync();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            Credential = new PreauthorizedTokenCredential(result.AccessToken);
        }

        public TokenCredential Credential { get; private set; }
    }
}
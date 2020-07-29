using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Identity.Client;
using Xamarin.Forms;
using XFBlogClient.Models;

namespace XFBlogClient.ViewModels
{
    public class LoadingViewModel : BaseViewModel
    {
        public async void Init()
        {
            bool isAuthenticated = false;
            try
            {
                if (App.AuthenticationClient != null)
                {
                    var accounts = await App.AuthenticationClient.GetAccountsAsync();
                    var result = await App.AuthenticationClient.AcquireTokenSilent(Constants.Scopes, accounts.FirstOrDefault()).ExecuteAsync();
                    if (!string.IsNullOrWhiteSpace(result?.AccessToken))
                    {
                        isAuthenticated = true;
                    }
                }
            }
            catch (MsalUiRequiredException ex)
            {
            }

            if (isAuthenticated)
            {
                await Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                await Shell.Current.GoToAsync("//LoginPage");
            }
        }
    }
}

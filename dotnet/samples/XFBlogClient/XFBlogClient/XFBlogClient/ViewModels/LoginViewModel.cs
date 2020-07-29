using XFBlogClient.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XFBlogClient.Models;
using XFBlogClient.Services;

namespace XFBlogClient.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            var dataService = DependencyService.Get<IDataStore<BlogPost>>();
            await dataService.LoginAsync();
            await dataService.GetItemsAsync(forceRefresh:true);
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
    }
}

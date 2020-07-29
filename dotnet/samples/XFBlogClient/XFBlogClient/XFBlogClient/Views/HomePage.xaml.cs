using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFBlogClient.ViewModels;

namespace XFBlogClient.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = BindingContext as HomeViewModel;
            //await vm.InitAsync();
        }

        
    }
}
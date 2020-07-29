using System;
using Microsoft.Identity.Client;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFBlogClient.Models;
using XFBlogClient.Services;
using XFBlogClient.Views;

namespace XFBlogClient
{
    public partial class App : Application
    {
        public static IPublicClientApplication? AuthenticationClient { get; private set; }
        public static object? UIParent { get; set; } = null;
        public App()
        {
            Device.SetFlags(new[] {
                "CarouselView_Experimental",
                "IndicatorView_Experimental",
                "SwipeView_Experimental",
                "CollectionView_Experimental",
                "AppTheme_Experimental"
            });


            AuthenticationClient = PublicClientApplicationBuilder.Create(Constants.ClientId)
                .WithAuthority("https://login.microsoftonline.com/common/v2.0")
                
                .WithParentActivityOrWindow(() => UIParent)
                //.WithIosKeychainSecurityGroup(Constants.IosKeychainSecurityGroups)
                //.WithB2CAuthority(Constants.AuthoritySignin)
                //.WithParentActivityOrWindow(() => UIParent)
                .WithRedirectUri($"msal{Constants.ClientId}://auth")
                .Build();

            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

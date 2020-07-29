using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Bogus;
using MvvmHelpers;
using Xamarin.Essentials;
using Xamarin.Forms;
using XFBlogClient.Models;
using XFBlogClient.Services;
using XFBlogClient.Views;

namespace XFBlogClient.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ObservableRangeCollection<BlogPost> BlogPosts { get; set; }

        public Command LoginCommand { get; set; }
        private string _avatarUrl;
        public string AvatarUrl
        {
            get => _avatarUrl;
            set => SetProperty(ref _avatarUrl, value);
        }

        private BlogPost _selectedBlogPost;
        public BlogPost SelectedBlogPost
        {
            get => _selectedBlogPost;
            set => SetProperty(ref _selectedBlogPost, value);
        }

        public Command ShowBlogPost { get; set; }

        public HomeViewModel()
        {
            LoginCommand = new Command(async () => OnLogin());
            ShowBlogPost = new Command(async (post) => await OnShowBlogPost());
            Title = "Home";

            AvatarUrl = new Faker().Internet.Avatar();
            BlogPosts = new ObservableRangeCollection<BlogPost>();
            
        }

        public async Task InitAsync()
        {
            var dataStore = DependencyService.Get<IDataStore<BlogPost>>();

            var blogPosts = await dataStore.GetItemsAsync();
            BlogPosts.ReplaceRange(blogPosts);
        }

        private async Task OnLogin()
        {
            var dataService = DependencyService.Get<IDataStore<BlogPost>>();
            await dataService.LoginAsync();

            var blogPosts = await dataService.GetItemsAsync();
            //await AddPosts(dataService);
            BlogPosts.ReplaceRange(blogPosts);
        }

        private async Task AddPosts(IDataStore<BlogPost> dataService)
        {
            try
            {
                await dataService.AddItemAsync(new BlogPost()
                {
                    Title = "Xamarin University Presents: Ship better apps with Visual Studio App Center",
                    Data = "At Microsoft Connect(); last November, we announced the general availability of Visual Studio App Center to help (Obj-C, Swift, Java, React Native, and Xamarin) developers quickly build, test, deploy, monitor, and improve their phone, tablet, desktop, and connected device apps with powerful, automated lifecycle services. As a .NET developer, you may already use the power of Visual Studio Tools for Xamarin to develop amazing apps in C#. Now, with Visual Studio App Center, you can easily tap into automated cloud services for every stage of your development process, so you get higher quality apps into your users' hands even faster. To learn how to simplify and automate your app development pipeline, join Mark Smith for \\\"Xamarin University Presents: Ship Better Apps with Visual Studio App Center\\\" on Thursday, January 25th at 9 am PT / 12 pm ET / 5 pm UTC. Mark will demo the services available in App Center – from setting up continuous cloud builds to automated testing and deployment to post-release crash reporting and aggregate user and app analytics. You'll leave ready to connect your first app and start improving your development process and your apps immediately. Jam-packed with step-by-step demos, this session has something for everyone, from app development beginners to seasoned pros who've built dozens of apps.",
                    ImageUrl = "https://devblogs.microsoft.com/visualstudio/wp-content/uploads/sites/4/2018/01/XamU-Presents-Ship-better-apps-with-Visual-Studio-App-Center.gif"
                });
                await dataService.AddItemAsync(new BlogPost()
                {
                    Title = "New Xamarin University Webinar: Exploring UrhoSharp 3D with Xamarin Workbooks",
                    Data = "Join me on Thursday, September 21 at 9 am PT / 12 pm ET / 4 pm UTC for my \\\"Exploring UrhoSharp 3D with Xamarin Workbooks\\\" webinar, where I'll combine my love of graphics programming with my passion for helping developers build better apps. Whether you're just getting started or an experienced developer, you'll learn how to use your .NET and C# skills to create native mobile apps, complete with 3D graphics.Lately, I've been exploring UrhoSharp, Xamarin's C# and .NET wrapper of Urho3D. Urho3D is a sophisticated, powerful open-source 3D game engine written in C++ — and it allows developers to build 3D visualization and augmented reality applications. You can easily integrate UrhoSharp code into your Windows .NET and Xamarin apps, including Android, iOS, Mac, and more. Part of the reason I'm excited about UrhoSharp: I love how graphics programming allows you to visualize something in your mind, then see it on the computer screen. About 10 years ago, I was heavy into 3D programming in the Windows Presentation Foundation, and I imagined a little two-lane highway in the form of a Möbius strip, with cars driving on the top, bottom, and sides. It looked great in theory, but I never found the time to code it. As I became more familiar with UrhoSharp, the Möbius mental image came back, and I was determined to make it real. I knew the math would be tricky, so I needed an immediate and interactive programming environment – and, with UrhoSharp and Xamarin Workbooks, my vision and my code came together. Xamarin Workbooks – an interactive blend of code and documentation that integrates C# code blocks into step-by-step guides – simplify learning mobile development, but it's also great for coding and testing out new ideas. As you add C# code blocks, you'll immediately see it executed on the screen, helping you learn as you go and make rapid changes to your apps' appearance. I'll dig into the code for this project, share my advice for getting started, and I – plus other Xamarin University instructors and mobile experts – will be on hand to answer your questions.",
                    ImageUrl = "https://devblogs.microsoft.com/wp-content/uploads/sites/4/2019/06/Urho-Sharp.png"
                });
                await dataService.AddItemAsync(new BlogPost()
                {
                    Title = "Develop ReactNative apps in Visual Studio Code",
                    Data = "ReactNative is a great way to build native, cross platform app for iOS and Android using JavaScript. We recently announced the launch of a Visual Studio Code Extension that enables you to build, debug and preview Apache Cordova apps. And today we're pleased to announce the availability a similar extension for ReactNative! ReactNative vs. Apache Cordova. Many of you may already be familiar with Apache Cordova as an open-source project that enables web developers to build mobile apps with full access to native APIs and offline support. In a Cordova app, the entire UI executes inside a full-screen WebView where you can leverage the same HTML, CSS and JS frameworks found on the web. But, since the UI is rendered in the WebView, it can be difficult if not impossible to achieve a truly native look and feel. ReactNative apps are also written with JavaScript – or, more specifically, they are written with the React/JSX framework. But, rather than run in a Webview like Cordova, code runs in a JavaScript engine that's bundled with the app. ReactNative then invokes native UI components (e.g. UITabBar on iOS and Drawer on Android) via JavaScript. This means that you can create native experiences that aren't possible with Cordova. That said, Apache Cordova is presently a more mature and stable technology that lets you write a common UI layer using web technologies, whereas ReactNative is much newer and still requires you to write distinct UI layers. If your app requires native UI and you enjoy the excitement of a rapidly evolving JavaScript platform, then ReactNative might be an option to consider.",
                    ImageUrl = "https://devblogs.microsoft.com/visualstudio/wp-content/uploads/sites/4/2016/02/2016-02-22-ReactNative-01.png"
                });
                await dataService.AddItemAsync(new BlogPost()
                {
                    Title = "10 user experience updates to the Azure portal",
                    Data = "We’re constantly working to improve your user experience in the Azure portal. Our goal is to offer you a productive and easy-to-use single-pane-of glass where you can build, manage, and monitor your Azure services, applications, and infrastructure. In this post, I’d like to share the highlights of our latest experience improvements, including Improved portal home experience: increased focus and clarity to bring services and instances that are relevant to you front and center. New service cards: new service hovercards that present contextual information relevant to each service. Enhanced service browsing experience: simplified offering navigation by progressively disclosing services.\r\nExtended Microsoft Learn integration: contextual integration of free training in key parts of the experience. Improved instance browsing experience: updated experience for more than 70 services with improved performance, better filtering and sorting options, grouping, and to allow exporting your resource lists to a CSV file.\r\nImproved Azure Resource Graph experience: re-use and share your queries via Resource Graph Saved Queries. Automatic refresh in Azure Dashboard: set automatic refresh intervals for your dashboard. Improved service icons: New icons re-designed for better visual consistency and reduced distractions. Simplified settings panel: better separation between general settings and localization. New landing page for Azure Mobile application: added a new landing page that brings important information.",
                    ImageUrl = "https://azurecomcdn.azureedge.net/mediahandler/acomblog/media/Default/blog/5bc5a049-0997-4970-b7b7-d3c0ddb6cfbe.png"
                });
                await dataService.AddItemAsync(new BlogPost()
                {
                    Title = "Essential tools and services for building mobile apps",
                    Data = "Azure, Visual Studio, Xamarin, and Visual Studio App Center give you the flexible, yet robust tools and services to build, test, deploy, and continuously improve Android and iOS apps that your users will love. Use your favorite language and tools, to tap into robust cloud services, and quickly scale to millions of users on demand.Cloud services for mobile developers\r\nAzure provides many services to help you build cloud-connected mobile apps, including Mobile Backend as a Service, Data, and Artificial Intelligence (outlined below), as well as services to support additional functionality, including Search, Identity, and Communication.Mobile Backend as a Service Azure Functions is a serverless backend for your mobile apps, where you just enter your code (whether C#, JavaScript, Python, or any other supported language) and it executes on demand, triggered by events you can define. It is a fast way to get backend code up and running. If you need a more complete solution, the Mobile Apps feature of Azure App Service provides you with a backend that can be written in C# or Node.js, giving you features such as data storage and offline sync of that data, user authentication, and push notifications. App Service backends come with autoscaling, setting you up for success when your app grows. Data Every cloud-connected app needs data storage, and Azure offers a range of solutions that fit your needs. With Azure Cosmos DB, you get extremely low latency and massively scalable database that's easily replicated worldwide. There is also Azure SQL Database, the intelligent, fully-managed relational cloud database, and a range of Azure Storage solutions to store files, unstructured objects, queues, or NoSQL databases. Artificial Intelligence Azure Machine Learning brings Artificial Intelligence capabilities to every developer. You can define learning models and perform computations on a massive scale. Azure Machine Learning Studio gives you an intuitive drag-and-drop interface that enables code-free experimentation. For more ready-to-use AI services, take a look at Cognitive Services, which offers a growing suite of services for image processing, natural language processing, speech recognition, and more.Cross-platform mobile app development with .NET\r\nWith Visual Studio and Xamarin, you can create mobile apps for Android and iOS while sharing up to 95%+ code using C#. With Azure, you can add turn-key, scalable, and flexible cloud services to keep users coming back for more with mobile-essential capabilities, like backend services, push notifications, data storage with offline sync, artificial intelligence, and much more.​",
                    ImageUrl = "https://gxcuf89792.i.lithium.com/t5/image/serverpage/image-id/185832i7F67E3BACBE1DA49?v=1.0"
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            



        }
        private async Task OnShowBlogPost()
        {
            await Shell.Current.GoToAsync($"{nameof(BlogDetailPage)}?{nameof(BlogDetailViewModel.ItemId)}={SelectedBlogPost.Id}");
        }
    }
}
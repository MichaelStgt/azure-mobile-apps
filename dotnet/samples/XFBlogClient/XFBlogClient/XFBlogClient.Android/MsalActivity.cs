using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace XFBlogClient.Droid
{
    [Activity]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
        DataHost = "auth",
        DataScheme = "msal50b7a23e-bb9e-42d5-91e9-a7eb77f45a3b")]
    public class MsalActivity : BrowserTabActivity
    {

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
        }
    }
}
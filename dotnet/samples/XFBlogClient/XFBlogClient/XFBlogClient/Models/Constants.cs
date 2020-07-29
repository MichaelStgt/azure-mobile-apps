namespace XFBlogClient.Models
{
    public static class Constants
    {
        public static string ClientId { get; } = "50b7a23e-bb9e-42d5-91e9-a7eb77f45a3b";
        //public static string[] Scopes { get; } = { "api://e924b7e6-3d2e-4a07-bec3-c89a299fa7b4/access_as_user" };
        public static string[] Scopes { get; } = { "" };

        public static string IosKeychainSecurityGroups { get; } = "com.microsoft.adalcache";
    }
}
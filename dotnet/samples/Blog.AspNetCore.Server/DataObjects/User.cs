using Azure.Mobile.Server.Entity;

namespace Blog.AspNetCore.Server.DataObjects
{
    public class User : EntityTableData
    {
        public string AvatarURL { get; set; }
        public string Username { get; set; }
    }
}

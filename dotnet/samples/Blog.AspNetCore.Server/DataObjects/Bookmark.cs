using Azure.Mobile.Server.Entity;

namespace Blog.AspNetCore.Server.DataObjects
{
    public class Bookmark : EntityTableData
    {
        public string PostId { get; set; }
        public string UserId { get; set; }
    }
}

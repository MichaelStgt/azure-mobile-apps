using Azure.Mobile.Server.Entity;
using Microsoft.AspNet.OData.Query;
using System;

namespace BlogServer.DataObjects
{
    [Filter]
    [OrderBy]
    public class BlogPost : EntityTableData
    {
        public string Title { get; set; }
        public string Data { get; set; }
        public string ImageUrl { get; set; }
        public int CommentCount { get; set; }
        public string OwnerId { get; set; }
        public DateTimeOffset PostedAt { get; set; }
    }
}

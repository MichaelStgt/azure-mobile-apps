﻿using System;

namespace XFBlogClient.Models
{
    public class BlogPost
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int CommentCount { get; set; }
        public bool ShowComments { get; set; }
        public string Data { get; set; }
        public string ImageUrl { get; set; }
        public string AuthorAvatarUrl { get; set; }
        public string AuthorName { get; set; }
        public DateTime PostedAt { get; set; }
        public bool IsBookmarked { get; set; }
    }
}
using System;

namespace Pressford.Web.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public int ArticleId { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
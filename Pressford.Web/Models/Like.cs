using System.Collections.Generic;

namespace Pressford.Web.Models
{
    public class Like
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ArticleId { get; set; }
    }
}
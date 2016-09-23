using System.Collections.Generic;

namespace Pressford.Web.Models
{
    public class Like
    {
        public int Id { get; set; }
        public Author LikeAuthor { get; set; }
        public bool Liked { get; set; }
    }
}
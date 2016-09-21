namespace Pressford.Web.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Author Author { get; set; }
    }
}
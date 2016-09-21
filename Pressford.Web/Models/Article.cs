using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pressford.Web.Models
{
    public class Article
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        public Author Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public List<Comment> Comments {get; set;}
    }
}
using Pressford.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pressford.Web.ViewModes
{
    public class ArticlePageViewModel
    {
        public string Title { get; set; }
        public List<Article> Articles { get; set; }
    }
}
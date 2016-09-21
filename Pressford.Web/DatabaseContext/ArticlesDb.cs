using Pressford.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Pressford.Web.DatabaseContext
{
    public class ArticlesDb : DbContext
    {
        public DbSet<Article> Articles { get; set; }

        
    }
}
using Pressford.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Pressford.Web.DatabaseContext
{
    public class CommentsDb : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
    }
}
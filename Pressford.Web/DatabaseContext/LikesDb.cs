using Pressford.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Pressford.Web.DatabaseContext
{
    public class LikesDb : DbContext
    {
        public DbSet<Like> Likes { get; set; }
    
    }
}
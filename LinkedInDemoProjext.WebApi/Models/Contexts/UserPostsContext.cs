using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkedInDemoProjext.WebApi.Models.Contexts
{
    public class UserPostsContext : DbContext
    {
        public UserPostsContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<UserPosts> UserPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

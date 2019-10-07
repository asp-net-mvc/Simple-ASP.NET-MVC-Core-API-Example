using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkedInDemoProjext.WebApi.Helpers;

namespace LinkedInDemoProjext.WebApi.Models.Contexts
{
    public class UsersContext : IdentityDbContext<Users>
    {
        public UsersContext(DbContextOptions<UsersContext> options):base(options)
        {

        }

        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>()
                .ToTable("Users");

            modelBuilder.Entity<Users>().HasData(new Users
            {
                Id = new Guid("755561b1-a76e-4d13-a4ad-c41920d6c4f2").ToString(),
                Name = "Sinan",
                Surname = "Şahin",
                UserName = "sinan.sahin",
                NormalizedUserName = "sinan.sahin",
                PasswordHash = PasswordHash.CreateMd5Hash("123456"),
                Gender = 'E',
                RecordTime = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString()
            }, new Users
            {
                Id = new Guid("c3fca1f0-9528-43c5-a2e6-510befb52908").ToString(),
                Name = "Şahin",
                Surname = "Sinan",
                UserName = "sahin.sinan",
                NormalizedUserName = "sahin.sinan",
                PasswordHash = PasswordHash.CreateMd5Hash("123456asd"),
                Gender = 'E',
                RecordTime = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString(),
            });
        }
    }
}

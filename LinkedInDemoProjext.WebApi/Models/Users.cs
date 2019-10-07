using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LinkedInDemoProjext.WebApi.Models
{
    public class Users : IdentityUser
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public char Gender { get; set; }
        public string Token { get; set; }
        public DateTime RecordTime { get; set; } = DateTime.Now;

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkedInDemoProjext.WebApi.Models
{
    public class UserPosts
    {
        [Key]
        public string Id { get; set; }
        public string refUserId { get; set; }
        public string Content { get; set; }
        public DateTime RecorTime { get; set; } = DateTime.Now;
    }
}

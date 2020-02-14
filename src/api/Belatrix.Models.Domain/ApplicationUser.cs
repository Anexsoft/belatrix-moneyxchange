using Microsoft.AspNetCore.Identity;
using System;

namespace Belatrix.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}

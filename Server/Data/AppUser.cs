using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Server.Data
{
    public class AppUser : IdentityUser
    {
        public string CoolName { get; set; }

        public AppUser(string userName) : base(userName)
        {
        }
    }
}

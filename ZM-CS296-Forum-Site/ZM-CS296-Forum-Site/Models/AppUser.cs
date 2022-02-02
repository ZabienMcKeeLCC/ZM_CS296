using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZM_CS296_Forum_Site.Models
{
    public class AppUser : IdentityUser
    {
        //[StringLength(20, MinimumLength = 5)]
        //public string Name { get; set; }
    }
}

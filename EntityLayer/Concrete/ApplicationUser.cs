using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;



namespace EntityLayer.Concrete
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public bool IsAdmin { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites.Identity
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { set; get; }
        public AddressI Address { set; get; }
    }
}

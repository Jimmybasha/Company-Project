using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.DAL.Model
{
    public class ApplicationUser:IdentityUser
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public bool IsAgree { get; set; }

	
	}
}

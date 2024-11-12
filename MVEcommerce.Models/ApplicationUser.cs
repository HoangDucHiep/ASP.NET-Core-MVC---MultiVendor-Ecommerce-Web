using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVEcommerce.Models
{
	public class ApplicationUser : IdentityUser
	{
		
		public string? FullName { get; set; }
		public string? Avatar { get; set; }
		public string? Gender { get; set; }
	}
}

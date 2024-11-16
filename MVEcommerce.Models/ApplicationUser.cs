using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

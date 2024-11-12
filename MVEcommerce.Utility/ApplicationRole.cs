using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVEcommerce.Utility
{
	///<summary>
	/// Provides constants for application roles and a method to seed roles.
	/// </summary>
	public class ApplicationRole
	{
		/// <summary>
		/// Admin role constant.
		/// </summary>
		public const string ADMIN = "Admin";

		/// <summary>
		/// Vendor role constant.
		/// </summary>
		public const string VENDOR = "Vendor";

		/// <summary>
		/// User role constant.
		/// </summary>
		public const string USER = "User";

		/// <summary>
		/// Seeds the roles into the role manager if they do not already exist.
		/// </summary>
		/// <param name="roleManager">The role manager to seed roles into.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
		{
			var roles = new[] { ADMIN, VENDOR, USER };

			foreach (var role in roles)
			{
				if (!await roleManager.RoleExistsAsync(role))
				{
					await roleManager.CreateAsync(new IdentityRole(role));
				}
			}
		}
	}
}

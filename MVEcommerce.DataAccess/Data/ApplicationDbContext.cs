using Microsoft.EntityFrameworkCore;
using MVEcommerce.Models;

namespace MVEcommerce.DataAccess.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<Category> Categories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Specify the column type for the Price property in the Product entity
			modelBuilder.Entity<Product>()
				.Property(p => p.Price)
				.HasColumnType("decimal(18,2)");

			modelBuilder.Entity<Category>().HasData(
		// Danh mục Parent: Home & Garden
		new Category { CategoryId = 1, ParentCategory = null, Name = "Home & Garden", Slug = "home-garden-1", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 2, ParentCategory = 1, Name = "Appliances", Slug = "appliances-2", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 3, ParentCategory = 1, Name = "Bath", Slug = "bath-3", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 4, ParentCategory = 1, Name = "Bedding", Slug = "bedding-4", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 5, ParentCategory = 1, Name = "Cleaning Supplies", Slug = "cleaning-supplies-5", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 6, ParentCategory = 1, Name = "Event & Party Supplies", Slug = "event-party-supplies-6", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 7, ParentCategory = 1, Name = "Furniture", Slug = "furniture-7", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 8, ParentCategory = 1, Name = "Garden Supplies", Slug = "garden-supplies-8", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 9, ParentCategory = 1, Name = "Home Accessories", Slug = "home-accessories-9", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 10, ParentCategory = 1, Name = "Home Décor", Slug = "home-decor-10", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 11, ParentCategory = 1, Name = "House Plants", Slug = "house-plants-11", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 12, ParentCategory = 1, Name = "Irons & Steamers", Slug = "irons-steamers-12", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 13, ParentCategory = 1, Name = "Kids Home", Slug = "kids-home-13", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 14, ParentCategory = 1, Name = "Kitchen & Dining", Slug = "kitchen-dining-14", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 15, ParentCategory = 1, Name = "Lamp & Lighting", Slug = "lamp-lighting-15", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 16, ParentCategory = 1, Name = "Rugs", Slug = "rugs-16", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 17, ParentCategory = 1, Name = "Storage & Organization", Slug = "storage-organization-17", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 18, ParentCategory = 1, Name = "Vacuums & Floor Care", Slug = "vacuums-floor-care-18", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 19, ParentCategory = 1, Name = "Wall Art", Slug = "wall-art-19", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },

		// Danh mục Parent: Electronics
		new Category { CategoryId = 20, ParentCategory = null, Name = "Electronics", Slug = "electronics-20", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 21, ParentCategory = 20, Name = "Accessories & Supplies", Slug = "accessories-supplies-21", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 22, ParentCategory = 20, Name = "Portable Audio & Video", Slug = "portable-audio-video-22", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 23, ParentCategory = 20, Name = "Security & Surveillance", Slug = "security-surveillance-23", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 24, ParentCategory = 20, Name = "Service Plans", Slug = "service-plans-24", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 25, ParentCategory = 20, Name = "Television & Video", Slug = "television-video-25", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 26, ParentCategory = 20, Name = "Camera & Photo", Slug = "camera-photo-26", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 27, ParentCategory = 20, Name = "Car & Vehicle Electronics", Slug = "car-vehicle-electronics-27", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 28, ParentCategory = 20, Name = "Cell Phones", Slug = "cell-phones-28", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 29, ParentCategory = 20, Name = "Chrome OS", Slug = "chrome-os-29", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 30, ParentCategory = 20, Name = "Computers & Accessories", Slug = "computers-accessories-30", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 31, ParentCategory = 20, Name = "eBook Readers", Slug = "ebook-readers-31", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 32, ParentCategory = 20, Name = "GPS & Navigation", Slug = "gps-navigation-32", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 33, ParentCategory = 20, Name = "Headphones", Slug = "headphones-33", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 34, ParentCategory = 20, Name = "Home Audio", Slug = "home-audio-34", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 35, ParentCategory = 20, Name = "Linux", Slug = "linux-35", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 36, ParentCategory = 20, Name = "Mac OS", Slug = "mac-os-36", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 37, ParentCategory = 20, Name = "Office Electronics", Slug = "office-electronics-37", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },

		// Danh mục Parent: Fashion
		new Category { CategoryId = 38, ParentCategory = null, Name = "Fashion", Slug = "fashion-38", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },

		// Danh mục Parent: Woman
		new Category { CategoryId = 39, ParentCategory = 38, Name = "Woman", Slug = "woman-39", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 40, ParentCategory = 39, Name = "Tops", Slug = "tops-40", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 41, ParentCategory = 39, Name = "Skirts", Slug = "skirts-41", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 42, ParentCategory = 39, Name = "Shoes", Slug = "shoes-42", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 43, ParentCategory = 39, Name = "Pants", Slug = "pants-43", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 44, ParentCategory = 39, Name = "Knitwear", Slug = "knitwear-44", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 45, ParentCategory = 39, Name = "Jeans", Slug = "jeans-45", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 46, ParentCategory = 39, Name = "Jackets", Slug = "jackets-46", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 47, ParentCategory = 39, Name = "Dresses", Slug = "dresses-47", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 48, ParentCategory = 39, Name = "Coats", Slug = "coats-48", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 49, ParentCategory = 39, Name = "Accessories", Slug = "accessories-49", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },

		// Danh mục Parent: Man
		new Category { CategoryId = 50, ParentCategory = null, Name = "Man", Slug = "man-50", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 51, ParentCategory = 50, Name = "Accessories", Slug = "accessories-51", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 52, ParentCategory = 50, Name = "Coats", Slug = "coats-52", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 53, ParentCategory = 50, Name = "Jackets", Slug = "jackets-53", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 54, ParentCategory = 50, Name = "Jeans", Slug = "jeans-54", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 55, ParentCategory = 50, Name = "Pants", Slug = "pants-55", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 56, ParentCategory = 50, Name = "Shirts", Slug = "shirts-56", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 57, ParentCategory = 50, Name = "Shoes", Slug = "shoes-57", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 58, ParentCategory = 50, Name = "Suits", Slug = "suits-58", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 59, ParentCategory = 50, Name = "Sweatshirts", Slug = "sweatshirts-59", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 60, ParentCategory = 50, Name = "T-Shirts", Slug = "t-shirts-60", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },

		// Danh mục Parent: Kids
		new Category { CategoryId = 61, ParentCategory = null, Name = "Kids", Slug = "kids-61", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 62, ParentCategory = 61, Name = "Accessories", Slug = "accessories-62", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 63, ParentCategory = 61, Name = "Sweatshirts", Slug = "sweatshirts-63", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 64, ParentCategory = 61, Name = "Baby Boy 3m-5y", Slug = "baby-boy-3m-5y-64", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 65, ParentCategory = 61, Name = "Baby Girl 3m-5y", Slug = "baby-girl-3m-5y-65", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 66, ParentCategory = 61, Name = "Boy 6-14y", Slug = "boy-6-14y-66", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 67, ParentCategory = 61, Name = "Girl 6-14y", Slug = "girl-6-14y-67", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 68, ParentCategory = 61, Name = "Mini 0-12m", Slug = "mini-0-12m-68", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },

		// Danh mục Parent: Jewelry & Accessories
		new Category { CategoryId = 69, ParentCategory = null, Name = "Jewelry & Accessories", Slug = "jewelry-accessories-69", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 70, ParentCategory = 69, Name = "Anklets", Slug = "anklets-70", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 71, ParentCategory = 69, Name = "Body Jewelry", Slug = "body-jewelry-71", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 72, ParentCategory = 69, Name = "Bracelets & Charms", Slug = "bracelets-charms-72", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 73, ParentCategory = 69, Name = "Brooches & Pins", Slug = "brooches-pins-73", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 74, ParentCategory = 69, Name = "Cleaners & Repair", Slug = "cleaners-repair-74", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 75, ParentCategory = 69, Name = "Earrings", Slug = "earrings-75", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 76, ParentCategory = 69, Name = "Necklaces", Slug = "necklaces-76", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 77, ParentCategory = 69, Name = "Rings", Slug = "rings-77", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 78, ParentCategory = 69, Name = "Sets", Slug = "sets-78", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 79, ParentCategory = 69, Name = "Watches", Slug = "watches-79", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 80, ParentCategory = 69, Name = "Wedding & Engagement", Slug = "wedding-engagement-80", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },

		// Danh mục Parent: Sports & Entertainment
		new Category { CategoryId = 81, ParentCategory = null, Name = "Sports & Entertainment", Slug = "sports-entertainment-81", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 82, ParentCategory = 81, Name = "Cycling", Slug = "cycling-82", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 83, ParentCategory = 81, Name = "Fitness", Slug = "fitness-83", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 84, ParentCategory = 81, Name = "Football", Slug = "football-84", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 85, ParentCategory = 81, Name = "Golf", Slug = "golf-85", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 86, ParentCategory = 81, Name = "Running", Slug = "running-86", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 87, ParentCategory = 81, Name = "Tennis", Slug = "tennis-87", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 88, ParentCategory = 81, Name = "Wrestling", Slug = "wrestling-88", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },

		// Danh mục Parent: Electronics
		new Category { CategoryId = 89, ParentCategory = null, Name = "Electronics", Slug = "electronics-89", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 90, ParentCategory = 89, Name = "Audio", Slug = "audio-90", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 91, ParentCategory = 89, Name = "Cameras", Slug = "cameras-91", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 92, ParentCategory = 89, Name = "Computers", Slug = "computers-92", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 93, ParentCategory = 89, Name = "Home Appliances", Slug = "home-appliances-93", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 94, ParentCategory = 89, Name = "Mobile Phones", Slug = "mobile-phones-94", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 95, ParentCategory = 89, Name = "Televisions", Slug = "televisions-95", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) },
		new Category { CategoryId = 96, ParentCategory = 89, Name = "Wearable Tech", Slug = "wearable-tech-96", Description = null, Status = "active", CreatedAt = new DateTime(2023, 1, 1), UpdatedAt = new DateTime(2023, 1, 1) }
	);
		}
	}
}
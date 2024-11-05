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
				new Category { CategoryId = 1, Name = "Home & Garden", Slug = "home-garden-1", BannerImage = "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/10/shop-head-bg-2.jpg?fit=1140%2C260&ssl=1", Status = "active", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) },

				// Danh mục Parent: Electronics
				new Category { CategoryId = 2, Name = "Electronics", Slug = "electronics-2", BannerImage = "https://motta.uix.store/wp-content/uploads/2022/07/shop_header.jpg", Status = "active", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) },

				// Danh mục Parent: Fashion
				new Category { CategoryId = 3, Name = "Fashion", Slug = "fashion-3", BannerImage = "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/10/shop-head-bg-3.jpg?fit=1140%2C260&ssl=1", Status = "active", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) },

				// Danh mục Parent: Jewelry & Accessories
				new Category { CategoryId = 4, Name = "Jewelry & Accessories", Slug = "jewelry-accessories-4", BannerImage = "https://motta.uix.store/wp-content/uploads/2022/07/shop_header.jpg", Status = "active", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) },

				// Danh mục Parent: Sports & Entertainment
				new Category { CategoryId = 5, Name = "Sports & Entertainment", Slug = "sports-entertainment-5", BannerImage = "https://motta.uix.store/wp-content/uploads/2022/07/shop_header.jpg", Status = "active", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) },

				// Danh mục Mother & Kids
				new Category { CategoryId = 6, Name = "Mother & Kids", Slug = "mother-kids-6", BannerImage = "https://motta.uix.store/wp-content/uploads/2022/07/shop_header.jpg", Status = "active", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) },

				// Danh mục Parent: Beauty & Health
				new Category { CategoryId = 7, Name = "Beauty & Health", Slug = "beauty-health-7", BannerImage = "https://motta.uix.store/wp-content/uploads/2022/07/shop_header.jpg", Status = "active", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) },

				// Danh mục Parent: Toys & Games
				new Category { CategoryId = 8, Name = "Toys & Games", Slug = "toys-games-8", BannerImage = "https://motta.uix.store/wp-content/uploads/2022/07/shop_header.jpg", Status = "active", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) },

				// Danh mục Parent: Automobiles & Motorcycles
				new Category { CategoryId = 9, Name = "Automobiles & Motorcycles", Slug = "automobiles-motorcycles-9", BannerImage = "https://motta.uix.store/wp-content/uploads/2022/07/shop_header.jpg", Status = "active", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) },

				// Danh mục Parent: Collectibles & Art
				new Category { CategoryId = 10, Name = "Collectibles & Art", Slug = "collectibles-art-10", BannerImage = "https://motta.uix.store/wp-content/uploads/2022/07/shop_header.jpg", Status = "active", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) },

				// Danh mục Parent: Tools & Home Improvement
				new Category { CategoryId = 11, Name = "Tools & Home Improvement", Slug = "tools-home-improvement-11", BannerImage = "https://motta.uix.store/wp-content/uploads/2022/07/shop_header.jpg", Status = "active", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) }
			);
		}
	}
}
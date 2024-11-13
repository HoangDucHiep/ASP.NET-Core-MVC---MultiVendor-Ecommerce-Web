using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;

namespace MVEcommerce.DataAccess.Data
{
	public class ApplicationDbContext : IdentityDbContext<IdentityUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<ProductVariant> ProductVariants { get; set; }
		public DbSet<ProductVariantOption> ProductVariantsOption { get; set; }

        // User Stuffs
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<Vendor> Vendors { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Specify the column type for the Price property in the Product entity
			modelBuilder.Entity<Product>()
				.Property(p => p.Price)
				.HasColumnType("decimal(18,2)");

			// Seed Category
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

			// Seed Vendor
			modelBuilder.Entity<Vendor>().HasData(
				new Vendor() { VendorId = 1, Name = "Truffles", Status = "active", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1), UserId = "43c87d11-b883-4c0e-bc91-e4936d67a7d4" }
			);

			// Seed Product
			modelBuilder.Entity<Product>().HasData(
				new Product() { ProductId = 1, VendorId = 1, CategoryId = 8, Name = "Batman Death Metal DC Comics Batman Figure", Slug = "batman-figure-metal-1", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel egestas dolor, nec dignissim metus.", Price = 100, Stock = 100, Sale = 0, SKU = "SKU-1", HasVariant = false, Status = "active", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) },
				new Product() { ProductId = 2, VendorId = 1, CategoryId = 8, Name = "Minions Toy with Buildable Figures (876 Pieces)", Slug = "minions-toy-figures-2", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel egestas dolor, nec dignissim metus.", Price = 200, Stock = 100, Sale = 0,SKU = "SKU-2", HasVariant = false, Status = "active", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) },
				new Product() { ProductId = 3, VendorId = 1, CategoryId = 8, Name = "Masters of the Universe Origins Skeletor Action Figure", Slug = "skeletor-action-figure-3", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vel egestas dolor, nec dignissim metus.", Price = 300, Sale = 26, Stock = 100, SKU = "SKU-3", HasVariant = false, Status = "active", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) },
				new Product() { ProductId = 4, VendorId = 1, CategoryId = 8, Name = "Apple – iPhone 11 64GB", Slug = "iphone-11-64gb", Description = "Lorem ipsum dolor sit amet-4, consectetur adipiscing elit. Donec vel egestas dolor, nec dignissim metus.", Price = null, Stock = null, SKU = null, HasVariant = true, Status = "active", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) }
			);

			// Seed ProductVariant
			modelBuilder.Entity<ProductVariant>().HasData(
				new ProductVariant() { VariantId = 1, ProductId = 4, Name = "Color", Status = "active", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) }
			);

			// Seed ProductVariantOption
			modelBuilder.Entity<ProductVariantOption>().HasData(
				new ProductVariantOption() { OptionId = 1, VariantId = 1, Value = "Black", Price = 400, Stock = 100, SKU = "SKU-4-Black", Status = "active", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) },
				new ProductVariantOption() { OptionId = 2, VariantId = 1, Value = "Red", Price = 450, Stock = 100, SKU = "SKU-4-White", Status = "active", CreatedAt = new DateTime(2024, 1, 1), UpdatedAt = new DateTime(2024, 1, 1) }
			);

			// Seed ProductImage
			modelBuilder.Entity<ProductImage>().HasData(
				// product 1 images
				new ProductImage() { ImageId = 1, ProductId = 1, ImageUrl = "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/08/4-1.jpg?fit=1400%2C1400&ssl=1", IsMain = true },
				new ProductImage() { ImageId = 2, ProductId = 1, ImageUrl = "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/08/2-2.jpg?fit=1400%2C1400&ssl=1", IsMain = false },
				new ProductImage() { ImageId = 3, ProductId = 1, ImageUrl = "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/08/1-2.jpg?fit=1400%2C1400&ssl=1", IsMain = false },
				new ProductImage() { ImageId = 4, ProductId = 1, ImageUrl = "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/08/3-2.jpg?fit=1400%2C1400&ssl=1", IsMain = false },
				//product 2 images
				new ProductImage() { ImageId = 5, ProductId = 2, ImageUrl = "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/09/1-73.jpg?fit=1400%2C1400&ssl=1", IsMain = true },
				new ProductImage() { ImageId = 6, ProductId = 2, ImageUrl = "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/09/3-54.jpg?fit=1400%2C1400&ssl=1", IsMain = false },
				new ProductImage() { ImageId = 7, ProductId = 2, ImageUrl = "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/09/4-37.jpg?fit=1400%2C1400&ssl=1", IsMain = false },
				//product 3 images
				new ProductImage() { ImageId = 8, ProductId = 3, ImageUrl = "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/09/1-71.jpg?fit=1400%2C1400&ssl=1", IsMain = true },
				new ProductImage() { ImageId = 9, ProductId = 3, ImageUrl = "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/09/2-61.jpg?fit=1400%2C1400&ssl=1", IsMain = false },
				new ProductImage() { ImageId = 10, ProductId = 3, ImageUrl = "https://i0.wp.com/motta.uix.store/wp-content/uploads/2022/09/3-52.jpg?fit=1400%2C1400&ssl=1", IsMain = false },
				//product 4 and its variant images
				new ProductImage() { ImageId = 11, ProductId = 4, VariantOptionID = 1 ,ImageUrl = "https://i0.wp.com/motta.uix.store/wp-content/uploads/2019/01/1.jpeg?fit=1400%2C1400&ssl=1", IsMain = true },
				new ProductImage() { ImageId = 12, ProductId = 4, VariantOptionID = 1 ,ImageUrl = "https://i0.wp.com/motta.uix.store/wp-content/uploads/2019/01/2.jpeg?fit=1400%2C1400&ssl=1", IsMain = false },
				new ProductImage() { ImageId = 13, ProductId = 4, VariantOptionID = 1 ,ImageUrl = "https://i0.wp.com/motta.uix.store/wp-content/uploads/2019/01/3.jpeg?fit=1400%2C1400&ssl=1", IsMain = false },
				new ProductImage() { ImageId = 14, ProductId = 4, VariantOptionID = 2 ,ImageUrl = "https://i0.wp.com/motta.uix.store/wp-content/uploads/2023/02/1-1.jpg?fit=1400%2C1400&ssl=1", IsMain = false },
				new ProductImage() { ImageId = 15, ProductId = 4, VariantOptionID = 2 ,ImageUrl = "https://i0.wp.com/motta.uix.store/wp-content/uploads/2023/02/2-1.jpg?fit=1400%2C1400&ssl=1", IsMain = false },
				new ProductImage() { ImageId = 16, ProductId = 4, VariantOptionID = 2 ,ImageUrl = "https://i0.wp.com/motta.uix.store/wp-content/uploads/2023/02/3-1.jpg?fit=1400%2C1400&ssl=1", IsMain = false }
			);
		}
	}
}
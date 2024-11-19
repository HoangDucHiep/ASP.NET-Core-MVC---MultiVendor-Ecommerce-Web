using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;
using MVEcommerce.Utility;

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
		public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Address> Addresses { get; set; }

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
			
			modelBuilder.Entity<ShoppingCart>()
                .HasOne(sc => sc.Product)
                .WithMany()
                .HasForeignKey(sc => sc.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.ParentOrder)
                .WithMany(o => o.SubOrders)
                .HasForeignKey(o => o.ParentOrderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.NoAction);


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

			
		}

		public static async Task SeedAdminUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string adminEmail = "admin@example.com";
            string adminPassword = "Admin@123";

            if (userManager.Users.All(u => u.Email != adminEmail))
            {
                var adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await roleManager.CreateAsync(new IdentityRole(ApplicationRole.ADMIN));
                    await userManager.AddToRoleAsync(adminUser, ApplicationRole.ADMIN);
                }
            }
        }
	}
}
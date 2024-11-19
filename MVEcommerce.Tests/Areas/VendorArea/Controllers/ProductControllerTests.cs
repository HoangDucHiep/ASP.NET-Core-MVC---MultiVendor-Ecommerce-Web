
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Moq;
using MVEcommerce.Areas.VendorArea.Controllers;
using MVEcommerce.DataAccess.Data;
using MVEcommerce.DataAccess.Repositoies.IRepositories;
using MVEcommerce.Models;
using MVEcommerce.Models.ViewModels.VendorProduct;
using MVEcommerce.Utility;
using Slugify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace MVEcommerce.Tests.Areas.VendorArea.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<UserManager<IdentityUser>> _mockUserManager;
        private readonly Mock<ApplicationDbContext> _mockDbContext;
        private readonly Mock<IWebHostEnvironment> _mockWebHostEnvironment;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUserManager = new Mock<UserManager<IdentityUser>>(Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            _mockDbContext = new Mock<ApplicationDbContext>();
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();

            _controller = new ProductController(_mockUnitOfWork.Object, _mockUserManager.Object, _mockDbContext.Object, _mockWebHostEnvironment.Object);
        }

        [Fact]
        public void Index_ReturnsViewResult_WithVendorProductIndexVM()
        {
            // Arrange
            var userId = "test-user-id";
            var vendor = new Vendor { VendorId = 1, UserId = userId };
            var products = new List<Product>
            {
                new Product { ProductId = 1, VendorId = vendor.VendorId, Name = "Product1", Status = ProductStatus.ACTIVE },
                new Product { ProductId = 2, VendorId = vendor.VendorId, Name = "Product2", Status = ProductStatus.ACTIVE }
            };

            _mockUnitOfWork.Setup(u => u.Vendor.Get(It.IsAny<Func<Vendor, bool>>())).Returns(vendor);
            _mockUnitOfWork.Setup(u => u.Product.GetAll(It.IsAny<Func<Product, bool>>(), It.IsAny<string>())).Returns(products.AsQueryable());

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = claimsPrincipal } };

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<VendorProductIndexVM>(viewResult.Model);
            Assert.Equal(2, model.Products.Count());
        }

        [Fact]
        public void AddProduct_ReturnsViewResult_WithVendorAddProductVM()
        {
            // Arrange
            _mockUnitOfWork.Setup(u => u.Category.GetAll()).Returns(new List<Category>
            {
                new Category { CategoryId = 1, Name = "Category1" },
                new Category { CategoryId = 2, Name = "Category2" }
            }.AsQueryable());

            // Act
            var result = _controller.AddProduct(null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<VendorAddProductVM>(viewResult.Model);
            Assert.Equal(2, model.Categories.Count());
        }

        [Fact]
        public async Task AddProduct_Post_ReturnsRedirectToActionResult_WhenModelStateIsValid()
        {
            // Arrange
            var userId = "test-user-id";
            var vendor = new Vendor { VendorId = 1, UserId = userId };
            var vm = new VendorAddProductVM
            {
                Product = new Product { Name = "Test Product" },
                ProductVariant = new ProductVariant(),
                ProductVariantOptions = new List<ProductVariantOption>()
            };

            _mockUnitOfWork.Setup(u => u.Vendor.Get(It.IsAny<Func<Vendor, bool>>())).Returns(vendor);
            _mockUnitOfWork.Setup(u => u.Product.Add(It.IsAny<Product>()));
            _mockUnitOfWork.Setup(u => u.Save());

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = claimsPrincipal } };

            // Act
            var result = await _controller.AddProduct(vm, null, null);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(ProductController.Index), redirectToActionResult.ActionName);
        }

        [Fact]
        public void EditProduct_ReturnsViewResult_WithVendorEditProductVM()
        {
            // Arrange
            var product = new Product
            {
                ProductId = 1,
                Name = "Test Product",
                ProductVariants = new List<ProductVariant>
                {
                    new ProductVariant
                    {
                        VariantId = 1,
                        Name = "Variant1",
                        ProductVariantOptions = new List<ProductVariantOption>
                        {
                            new ProductVariantOption { OptionId = 1, Value = "Option1" }
                        }
                    }
                }
            };

            _mockUnitOfWork.Setup(u => u.Product.Get(It.IsAny<Func<Product, bool>>(), It.IsAny<string>())).Returns(product);
            _mockUnitOfWork.Setup(u => u.Category.GetAll()).Returns(new List<Category>
            {
                new Category { CategoryId = 1, Name = "Category1" },
                new Category { CategoryId = 2, Name = "Category2" }
            }.AsQueryable());

            // Act
            var result = _controller.EditProduct(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<VendorEditProductVM>(viewResult.Model);
            Assert.Equal(2, model.Categories.Count());
            Assert.Equal(1, model.Product.ProductId);
        }

        // Additional tests for other actions can be added here...
    }
}
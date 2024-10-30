﻿// <auto-generated />
using System;
using MVEcommerce.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MVEcommerce.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241030171842_SeedCategoryData")]
    partial class SeedCategoryData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MVEcommerce.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentCategory")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("CategoryId");

                    b.HasIndex("ParentCategory");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MVEcommerce.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasVariant")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SKU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Stock")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("VendorId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("VendorId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("MVEcommerce.Models.ProductVariant", b =>
                {
                    b.Property<int>("VariantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VariantId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("VariantId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductVariant");
                });

            modelBuilder.Entity("MVEcommerce.Models.ProductVariantOption", b =>
                {
                    b.Property<int>("OptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OptionId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SKU")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VariantId")
                        .HasColumnType("int");

                    b.HasKey("OptionId");

                    b.HasIndex("VariantId");

                    b.ToTable("ProductVariantOption");
                });

            modelBuilder.Entity("MVEcommerce.Models.Vendor", b =>
                {
                    b.Property<int>("VendorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VendorId"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("VendorId");

                    b.ToTable("Vendor");
                });

            modelBuilder.Entity("MVEcommerce.Models.Category", b =>
                {
                    b.HasOne("MVEcommerce.Models.Category", "ParentCategoryNavigation")
                        .WithMany("ChildCategories")
                        .HasForeignKey("ParentCategory");

                    b.Navigation("ParentCategoryNavigation");
                });

            modelBuilder.Entity("MVEcommerce.Models.Product", b =>
                {
                    b.HasOne("MVEcommerce.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MVEcommerce.Models.Vendor", "Vendor")
                        .WithMany("Products")
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("MVEcommerce.Models.ProductVariant", b =>
                {
                    b.HasOne("MVEcommerce.Models.Product", "Product")
                        .WithMany("ProductVariants")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("MVEcommerce.Models.ProductVariantOption", b =>
                {
                    b.HasOne("MVEcommerce.Models.ProductVariant", "ProductVariant")
                        .WithMany("ProductVariantOptions")
                        .HasForeignKey("VariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductVariant");
                });

            modelBuilder.Entity("MVEcommerce.Models.Category", b =>
                {
                    b.Navigation("ChildCategories");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("MVEcommerce.Models.Product", b =>
                {
                    b.Navigation("ProductVariants");
                });

            modelBuilder.Entity("MVEcommerce.Models.ProductVariant", b =>
                {
                    b.Navigation("ProductVariantOptions");
                });

            modelBuilder.Entity("MVEcommerce.Models.Vendor", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
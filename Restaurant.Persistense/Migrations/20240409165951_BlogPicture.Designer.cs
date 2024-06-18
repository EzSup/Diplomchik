﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Restaurant.Persistense;

#nullable disable

namespace Restaurant.Persistense.Migrations
{
    [DbContext(typeof(RestaurantDbContext))]
    [Migration("20240409165951_BlogPicture")]
    partial class BlogPicture
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Restaurant.Core.Models.Bill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CartId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("OrderDateAndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("PaidAmount")
                        .HasColumnType("numeric");

                    b.Property<Guid>("TableId")
                        .HasColumnType("uuid");

                    b.Property<int>("TipsPercents")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CartId")
                        .IsUnique();

                    b.HasIndex("CustomerId");

                    b.HasIndex("TableId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Blog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImageLink")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DiscountId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DiscountId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Cuisine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DiscountId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DiscountId")
                        .IsUnique();

                    b.ToTable("Cuisines");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CartId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhotoLink")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CartId")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Discount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("PecentsAmount")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Dish", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Available")
                        .HasColumnType("boolean");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CuisineId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DiscountId")
                        .HasColumnType("uuid");

                    b.Property<string[]>("IngredientsList")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string[]>("PhotoLinks")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CuisineId");

                    b.HasIndex("DiscountId");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("Restaurant.Core.Models.DishCart", b =>
                {
                    b.Property<Guid>("DishId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CartId")
                        .HasColumnType("uuid");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.HasKey("DishId", "CartId");

                    b.HasIndex("CartId");

                    b.ToTable("DishCarts");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<Guid>("DishId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("Posted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double?>("Rate")
                        .HasColumnType("double precision");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("DishId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Restaurant.Core.Models.RolePermission", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("PermissionId")
                        .HasColumnType("integer");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Table", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Free")
                        .HasColumnType("boolean");

                    b.Property<decimal>("PriceForHour")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("Restaurant.Core.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNum")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PhoneNum")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Restaurant.Core.Models.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Bill", b =>
                {
                    b.HasOne("Restaurant.Core.Models.Cart", "Cart")
                        .WithOne("Bill")
                        .HasForeignKey("Restaurant.Core.Models.Bill", "CartId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Restaurant.Core.Models.Customer", "Customer")
                        .WithMany("Bills")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Restaurant.Core.Models.Table", "Table")
                        .WithMany("Bills")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Customer");

                    b.Navigation("Table");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Category", b =>
                {
                    b.HasOne("Restaurant.Core.Models.Discount", "Discount")
                        .WithMany("Categories")
                        .HasForeignKey("DiscountId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Discount");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Cuisine", b =>
                {
                    b.HasOne("Restaurant.Core.Models.Discount", "Discount")
                        .WithOne("Cuisine")
                        .HasForeignKey("Restaurant.Core.Models.Cuisine", "DiscountId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Discount");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Customer", b =>
                {
                    b.HasOne("Restaurant.Core.Models.Cart", "Cart")
                        .WithOne("Customer")
                        .HasForeignKey("Restaurant.Core.Models.Customer", "CartId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Restaurant.Core.Models.User", "User")
                        .WithOne("Customer")
                        .HasForeignKey("Restaurant.Core.Models.Customer", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Dish", b =>
                {
                    b.HasOne("Restaurant.Core.Models.Category", "Category")
                        .WithMany("Dishes")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("Restaurant.Core.Models.Cuisine", "Cuisine")
                        .WithMany("Dishes")
                        .HasForeignKey("CuisineId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("Restaurant.Core.Models.Discount", "Discount")
                        .WithMany("Dishes")
                        .HasForeignKey("DiscountId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Cuisine");

                    b.Navigation("Discount");
                });

            modelBuilder.Entity("Restaurant.Core.Models.DishCart", b =>
                {
                    b.HasOne("Restaurant.Core.Models.Cart", "Cart")
                        .WithMany("DishCarts")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Restaurant.Core.Models.Dish", "Dish")
                        .WithMany("DishCarts")
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Dish");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Review", b =>
                {
                    b.HasOne("Restaurant.Core.Models.Customer", "Author")
                        .WithMany("Reviews")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Restaurant.Core.Models.Dish", "Dish")
                        .WithMany("Reviews")
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Dish");
                });

            modelBuilder.Entity("Restaurant.Core.Models.RolePermission", b =>
                {
                    b.HasOne("Restaurant.Core.Models.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Restaurant.Core.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Restaurant.Core.Models.UserRole", b =>
                {
                    b.HasOne("Restaurant.Core.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Restaurant.Core.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Restaurant.Core.Models.Cart", b =>
                {
                    b.Navigation("Bill");

                    b.Navigation("Customer");

                    b.Navigation("DishCarts");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Category", b =>
                {
                    b.Navigation("Dishes");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Cuisine", b =>
                {
                    b.Navigation("Dishes");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Customer", b =>
                {
                    b.Navigation("Bills");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Discount", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Cuisine");

                    b.Navigation("Dishes");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Dish", b =>
                {
                    b.Navigation("DishCarts");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Restaurant.Core.Models.Table", b =>
                {
                    b.Navigation("Bills");
                });

            modelBuilder.Entity("Restaurant.Core.Models.User", b =>
                {
                    b.Navigation("Customer");
                });
#pragma warning restore 612, 618
        }
    }
}

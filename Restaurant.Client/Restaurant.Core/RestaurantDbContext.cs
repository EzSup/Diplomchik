using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Models;
using Table = Microsoft.EntityFrameworkCore.Metadata.Internal.Table;

namespace Restaurant.Core;

public class RestaurantDbContext : DbContext
{
    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
    {
    }

    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Cuisine> Cuisines { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Models.Table> Tables { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    //public DbSet<DishBill> DishBills { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<DishCart> DishCarts { get; set; }
    public DbSet<Blog> Blogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Name)
                .IsRequired();
            entity.Property(d => d.Price)
                .IsRequired();
            entity.Property(d => d.Available)
                .IsRequired();

            entity.HasOne(d => d.Category)
                .WithMany(c => c.Dishes)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(d => d.Cuisine)
                .WithMany(c => c.Dishes)
                .HasForeignKey(d => d.CuisineId)
                .OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(d => d.Discount)
                .WithMany(dis => dis.Dishes)
                .HasForeignKey(d => d.DiscountId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.PecentsAmount)
                .IsRequired();

            entity.HasOne(c => c.Cuisine)
                .WithOne(d => d.Discount)
                .HasForeignKey<Cuisine>(c => c.DiscountId)
                .OnDelete(DeleteBehavior.SetNull);
        });
        
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name)
                .IsRequired();

            entity.HasOne(c => c.Discount)
                .WithMany(d => d.Categories)
                .HasForeignKey(c => c.DiscountId)
                .OnDelete(DeleteBehavior.NoAction);
        });
        
        modelBuilder.Entity<Cuisine>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name)
                .IsRequired();
        });
        
        modelBuilder.Entity<Models.Table>(entity =>
        {
            entity.HasKey(l => l.Id);
            entity.Property(l => l.PriceForHour)
                .IsRequired();
            
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.OrderDateAndTime)
                .IsRequired();
            entity.Property(b => b.PaidAmount)
                .IsRequired();
            entity.HasOne(b => b.Customer)
                .WithMany(c => c.Bills)
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(b => b.Table)
                .WithMany(t => t.Bills)
                .HasForeignKey(b => b.TableId)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(b => b.Cart)
                .WithOne(c => c.Bill)
                .HasForeignKey<Bill>(b => b.CartId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(r => r.Id);

            entity.HasOne(r => r.Author)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(r => r.Dish)
                .WithMany(d => d.Reviews)
                .HasForeignKey(r => r.DishId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name)
                .IsRequired();
            entity.Property(c => c.Password)
                .IsRequired();

            entity.HasOne(c => c.Cart)
            .WithOne(c => c.Customer)
            .HasForeignKey<Customer>(c => c.CartId)
            .OnDelete(DeleteBehavior.NoAction);
            
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(c => c.Id);
        });

        modelBuilder.Entity<DishCart>(entity =>
        {
            entity.HasKey(dc => new { dc.DishId, dc.CartId });

            entity.HasOne(dc => dc.Cart)
            .WithMany(c => c.DishCarts)
            .HasForeignKey(dc => dc.CartId)
            .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(dc => dc.Dish)
            .WithMany(d => d.DishCarts)
            .HasForeignKey(dc => dc.DishId)
            .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.Id);
        });


    }
}
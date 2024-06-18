using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;

namespace Restaurant.Persistense.Configurations
{
    public class DishCartConfiguration : IEntityTypeConfiguration<DishCart>
    {
        public void Configure(EntityTypeBuilder<DishCart> builder)
        {
            builder.HasKey(b => new { b.DishId, b.CartId});

            builder.Property(b => b.Count)
                .IsRequired();

            builder.HasOne(dc => dc.Cart)
                .WithMany(c => c.DishCarts)
                .HasForeignKey(dc => dc.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(dc => dc.Dish)
                .WithMany(d => d.DishCarts)
                .HasForeignKey(dc => dc.DishId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

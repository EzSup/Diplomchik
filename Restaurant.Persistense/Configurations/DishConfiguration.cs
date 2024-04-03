using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Models;

namespace Restaurant.Persistense.Configurations
{
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name)
                .IsRequired();
            builder.Property(d => d.Price)
                .IsRequired();

            builder.HasOne(d => d.Category)
                .WithMany(c => c.Dishes)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(d => d.Cuisine)
                .WithMany(c => c.Dishes)
                .HasForeignKey(d => d.CuisineId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(d => d.Discount)
                .WithMany(dis => dis.Dishes)
                .HasForeignKey(d => d.DiscountId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

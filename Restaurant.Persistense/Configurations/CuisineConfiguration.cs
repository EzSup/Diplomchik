using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;

namespace Restaurant.Persistense.Configurations
{
    public class CuisineConfiguration : IEntityTypeConfiguration<Cuisine>
    {
        public void Configure(EntityTypeBuilder<Cuisine> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(c => c.Name)
                .IsRequired();

            builder.HasOne(b => b.Discount)
                .WithOne(c => c.Cuisine)
                .HasForeignKey<Cuisine>(c => c.DiscountId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Navigation(d => d.Discount).IsRequired(false);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;

namespace Restaurant.Persistense.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(c => c.Name)
                .IsRequired();

            builder.HasOne(b => b.Discount)
                .WithMany(c => c.Categories)
                .HasForeignKey(b => b.DiscountId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;

namespace Restaurant.Persistense.Configurations
{
    public class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(t => t.PriceForHour)
                .IsRequired();

            builder.Property(t => t.Num)
                .IsRequired();
        }
    }
}

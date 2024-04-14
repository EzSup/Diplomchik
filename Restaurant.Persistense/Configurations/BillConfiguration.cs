using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Persistense.Configurations
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.OrderDateAndTime)
                .IsRequired();

            builder.Property(b => b.PaidAmount)
                .IsRequired();

            builder.HasOne(b => b.Customer)
                .WithMany(c => c.Bills)
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.Cart)
                .WithOne(c => c.Bill)
                .HasForeignKey<Bill>(b => b.CartId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.Reservation)
                .WithOne(r => r.Bill)
                .HasForeignKey<Bill>(b => b.ReservationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(b => b.DeliveryData)
               .WithOne(r => r.Bill)
               .HasForeignKey<Bill>(b => b.DeliveryId)
               .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

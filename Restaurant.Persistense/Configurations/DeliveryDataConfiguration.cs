using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Persistense.Configurations
{
    public class DeliveryDataConfiguration : IEntityTypeConfiguration<DeliveryData>
    {
        public void Configure(EntityTypeBuilder<DeliveryData> builder)
        {
            builder.HasKey(b => b.Id);

        }
    }
}

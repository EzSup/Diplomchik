using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;

namespace Restaurant.Persistense.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.Email)
                .IsUnique();
            builder.HasIndex(u => u.PhoneNum)
                .IsUnique();
            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.HasOne(r => r.Customer)
                .WithOne(c => c.User)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<UserRole>(
                    l => l.HasOne<Role>().WithMany().HasForeignKey(r => r.RoleId),
                    r => r.HasOne<User>().WithMany().HasForeignKey(r => r.UserId));
        }
    }
}

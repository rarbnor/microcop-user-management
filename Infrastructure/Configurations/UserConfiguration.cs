using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
	public class UserConfiguration : BaseEntityTypeConfiguration<User>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);

            builder.Property(user => user.ApiKey).ValueGeneratedOnAdd().HasColumnType("uuid");

            builder.Property(user => user.Username).IsRequired();
            builder.HasIndex(user => user.Username).IsUnique();

            builder.Property(user => user.Fullname);

            builder.Property(user => user.Email).IsRequired();
            builder.HasIndex(user => user.Email).IsUnique();

            builder.Property(user => user.PasswordSalt).IsRequired();
            builder.Property(user => user.PasswordHash).IsRequired();

            builder.Property(user => user.MobileNumber);
            builder.Property(user => user.Language);
            builder.Property(user => user.Culture);

            builder.ToTable("Users");
        }
    }
}


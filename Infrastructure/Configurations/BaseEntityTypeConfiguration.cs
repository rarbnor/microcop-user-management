using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
	public abstract class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity 
	{
        public void Configure(EntityTypeBuilder<T> builder)
        {
            BaseConfigure(builder);
            ConfigureEntity(builder);
        }

        public void BaseConfigure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.InsertDate)
                .IsRequired();

            builder.Property(x => x.UpdateDate)
                .IsRequired();
        }

        protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);

    }
}


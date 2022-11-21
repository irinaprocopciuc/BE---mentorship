using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreWebAPI.Domain.Entities;

namespace StoreWebApi.Infrastructure.DataAccess.Configurations
{
    public class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
    {
        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.Product);
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
        }
    }
}

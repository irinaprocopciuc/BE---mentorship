using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreWebAPI.Domain.Entities;

namespace StoreWebApi.Infrastructure.DataAccess.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.HasOne(o => o.Cart);
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Adress).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Phone).HasMaxLength(50).IsRequired();
            builder.Property(p => p.PaymentMethod).HasMaxLength(50).IsRequired();
        }
    }
}

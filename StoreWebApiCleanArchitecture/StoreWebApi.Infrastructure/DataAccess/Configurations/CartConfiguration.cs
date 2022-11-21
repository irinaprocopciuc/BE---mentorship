using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreWebAPI.Domain.Entities;

namespace StoreWebApi.Infrastructure.DataAccess.Configurations
{
    public class CartConfiguration: IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasMany(c => c.Products);
            builder.Property(c => c.Total).HasColumnType("decimal(10,2)");
        }
    }
}

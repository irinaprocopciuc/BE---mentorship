using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Web_API.DataAccess.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.OrderId);
            builder.HasOne(o => o.Cart);
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Adress).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Phone).HasMaxLength(50).IsRequired();
            builder.Property(p => p.PaymentMethod).HasMaxLength(50).IsRequired();
        }
    }
}

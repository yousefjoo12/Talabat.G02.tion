using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Order_Aggregrate;

namespace Talabat.Repository.Data.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.Status)
                 .HasConversion(
                OStatus => OStatus.ToString(),
                OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus),OStatus));
            
            builder.OwnsOne(o => o.ShippingAddress, 
                ShippingAddress => ShippingAddress.WithOwner()); //total من الطرفين

            builder.Property(o => o.SubTotal)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(p=> p.DeliveryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}

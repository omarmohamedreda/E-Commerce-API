using ECommerce.Domain.Models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presistence.Configurations.Order_Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.Property(O => O.Subtotal).HasColumnType("decimal(8,2)");

            builder.HasOne(O=>O.DeliveryMethod)
                .WithMany().HasForeignKey(O=>O.DeliveryMethodId);

            builder.OwnsOne(O => O.Address);
        }
    }

}

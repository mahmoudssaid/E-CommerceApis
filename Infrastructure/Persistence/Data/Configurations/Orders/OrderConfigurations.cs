using Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderEntity = Domain.Entities.Order.Order;

namespace Persistence.Data.Configurations.Orders
{
    internal class OrderConfigurations : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.OwnsOne(order => order.ShippingAddress, address => address.WithOwner());

            builder.HasMany(order => order.OrderItems).WithOne();

            builder.Property(order => order.PaymentStatus).HasConversion(s => s.ToString(),
                                                                           s => Enum.Parse<OrderPaymentStatusEnum>(s));
            builder.HasOne(order => order.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);

            builder.Property(o => o.SubTotal).HasColumnType("decimal(18.3)");
        }
    }
}

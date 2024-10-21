using Domain.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Order
{
    public class Order : BaseEntity<Guid>
    {
        public Order() { }  
        public Order(string userEmail, Address shippingAddress, ICollection<OrderItem> orderItems, DeliveryMethod deliveryMethod, decimal subTotal)
        {
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            OrderItems = orderItems;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
        }

        public string UserEmail { get; set; }
        public Address ShippingAddress { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public DeliveryMethod DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }

        //sub total items.Quntity * price
        public decimal SubTotal { get; set; }

        //payment 
        public OrderPaymentStatusEnum PaymentStatus { get; set; } = OrderPaymentStatusEnum.Pending;
        public string PaymentIntentId { get; set; } = string.Empty;

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

    }
}

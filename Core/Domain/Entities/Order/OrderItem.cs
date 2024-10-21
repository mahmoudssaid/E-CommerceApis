using Domain.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Order
{
    public class OrderItem : BaseEntity<Guid>
    {
        public OrderItem() { }
        public OrderItem(ProductInOrderItem product, int quintity, decimal price)
        {
            this.product = product;
            Quintity = quintity;
            Price = price;
        }

        public ProductInOrderItem product { get; set; }
        public int Quintity { get; set; }
        public decimal Price { get; set; }
    }
}

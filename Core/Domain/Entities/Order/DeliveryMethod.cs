using Domain.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Order
{
    public class DeliveryMethod : BaseEntity<int>
    {
        public DeliveryMethod() { }
        public DeliveryMethod(string shortName, string description, string delivryTime, decimal price)
        {
            ShortName = shortName;
            Description = description;
            DelivryTime = delivryTime;
            Price = price;
        }

        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DelivryTime { get; set; }
        public decimal Price { get; set; }
    }
}

using Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class OrderWithIncludeSpecifications : Specifications<Order>
    {
        public OrderWithIncludeSpecifications(Guid id) : base(order => order.Id == id)
        {
            AddInclude(order => order.DeliveryMethodId);
            AddInclude(order => order.OrderItems);
        }

        public OrderWithIncludeSpecifications(string email) : base(order => order.UserEmail == email)
        {
            AddInclude(order => order.DeliveryMethodId);
            AddInclude(order => order.OrderItems);

            SetOrderBy(order => order.OrderDate);
        }
    }
}

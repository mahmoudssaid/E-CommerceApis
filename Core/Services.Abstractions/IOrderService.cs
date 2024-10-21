using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IOrderService
    {
        //Get Order by Id =>  OrderResult(Guid id )
        public Task<OrderResult> GetOrderByIdAsync(Guid id);


        //Get Orders for user By Email =>  IColection<OrderResult> (string Email)
        public Task<IEnumerable<OrderResult>> GetOrderByEmailAsync(string email);


        //Create Order => OrderResult(OrderRequst ,string email)
        public Task<OrderResult> CreateOrderAsync(OrderRequest request, string userEmail);


        //Get All Delivery Methods
        public Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync();
    }
}

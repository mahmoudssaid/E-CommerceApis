using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.OrderModels;
using System.Security.Claims;


namespace Presentation
{
    [Authorize]
    public class OrdersController(IServiceManager serviceManager) 
        : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<OrderRequest>> Creaate(OrderRequest request)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await serviceManager.orderService.CreateOrderAsync(request, email);
            return Ok(order);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResult>>> GetOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await serviceManager.orderService.GetOrderByEmailAsync(email);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderRequest>> Order(Guid id)
        {
            var order = await serviceManager.orderService.GetOrderByIdAsync(id);
            return Ok(order);
        }

        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<DeliveryMethodResult>> GetDelivryMethods()
        {
            var methods = await serviceManager.orderService.GetDeliveryMethodsAsync();
            return Ok(methods);
        }

    }
}

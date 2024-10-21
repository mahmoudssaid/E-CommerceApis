using Domain.Entities.Basket;
using Domain.Entities.Order;
using Domain.Entities.Productc;
using Domain.Exceptions;
using Services.Specifications;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class OrderService
        (IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepo) : IOrderService
    {
        public async Task<OrderResult> CreateOrderAsync(OrderRequest request, string userEmail)
        {
            //1-address 
            var address = mapper.Map<Address>(request.ShippingAddress);
            //2-orderItem => Basket => Basket item => order item
            var basket = await basketRepo.GetBasketAsync(request.BasketId)
                ?? throw new BasketNotFoundException(request.BasketId);
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await unitOfWork.GetRepository<Product, int>()
                    .GetAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);

                orderItems.Add(CreOrderItem(item, product));
            }

            //3- delivery 
            var delivery = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(request.DeliveryMethodId) ??
                throw new DeliveryMethodNotFoundException(request.DeliveryMethodId);

            //-4 SubTotal
            var subTotal = orderItems.Sum(item => item.Price * item.Quintity);

            //save to database
            var order = new Order(userEmail, address, orderItems, delivery, subTotal);

            await unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
            await unitOfWork.SaveChangesAsync();

            //map and return
            return mapper.Map<OrderResult>(order);
        }

        private OrderItem CreOrderItem(BasketItem item, Product product)
       => new OrderItem(new ProductInOrderItem(item.Id, product.Name, product.PictureUrl), item.Quantity, product.Price);

        public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethodResult>>(deliveryMethods);
        }

        public async Task<IEnumerable<OrderResult>> GetOrderByEmailAsync(string email)
        {
            var orders = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(new OrderWithIncludeSpecifications(email));

            return mapper.Map<IEnumerable<OrderResult>>(orders);
        }

        public async Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>().GetAsync(new OrderWithIncludeSpecifications(id))
                ?? throw new OrderNotFoundException(id);

            return mapper.Map<OrderResult>(order);
        }
    }
}

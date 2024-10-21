using Domain.Entities.Identity;
using Domain.Entities.Order;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Extensions.Options;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Domain.Entities.Order.Address, AddressDTO>();
            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(d => d.ProductId, options => options.MapFrom(s => s.product.ProductId))
                .ForMember(d => d.ProductName, options => options.MapFrom(s => s.product.ProductName))
                .ForMember(d => d.PictureUrl, options => options.MapFrom(s => s.product.PictureUrl));



            CreateMap<Order, OrderResult>()
                .ForMember(d => d.PaymentStatus, options => options.MapFrom(s => s.ToString()))
                .ForMember(d => d.DeliveryMethod, options => options.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.Total, options => options.MapFrom(s => s.SubTotal + s.DeliveryMethod.Price));


            CreateMap<DeliveryMethod, DeliveryMethodResult>();
        }
    }
}

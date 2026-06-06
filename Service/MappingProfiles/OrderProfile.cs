using AutoMapper;
using DomainLayer.Models.OrderModule;
using Shared.DTOs;
using Shared.DTOs.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDTO, OrderAddress>().ReverseMap();

            
            CreateMap<Order, OrderDTO>()
                .ForMember(D => D.DeliveryMethod
                , O => 
                O.MapFrom(S => S.DeliveryMethod.ShortName));

            
            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(D => D.ProductName
                , O =>
                O.MapFrom(S => S.Product.ProductName))
                .ForMember(D => D.PictureUrl
                , O =>
                O.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(D => D.DeliveryMethod,
        O => O.MapFrom(S => S.DeliveryMethod.ShortName))
                .ForMember(D => D.OrderStatus,
        O => O.MapFrom(S => S.OrderStatus.ToString()))
                .ForMember(D => D.Total,
        O => O.MapFrom(S => S.GetTotal()));


            CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();
        }
    }
}

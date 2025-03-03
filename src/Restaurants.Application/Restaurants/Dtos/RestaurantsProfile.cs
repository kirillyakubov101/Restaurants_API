using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.ModifyRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantsProfile : Profile
{
    public RestaurantsProfile()
    {
        CreateMap<UpdateRestaurantCommand, Restaurant>()
            .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name == null ? null : src.Name))
            .ForMember(d => d.Description, opt => opt.MapFrom(src => src.Description == null ? null : src.Description))
            .ForMember(d => d.HasDelivery, opt => opt.MapFrom(src => src.HasDelivery == false ? false : src.HasDelivery));



        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(d => d.City,
                opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
         .ForMember(d => d.PostalCode,
                opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
          .ForMember(d => d.Street,
                opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
         .ForMember(d => d.Dishes, opt => opt.MapFrom(src => src.Dishes));

        CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(d => d.Address, opt => opt.MapFrom(src => new Address
            {
                City = src.City,
                PostalCode = src.PostalCode,
                Street = src.Street
            }));
    }
}
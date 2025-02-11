using AutoMapper;
using LosPollos.Application.DTOs;
using LosPollos.Domain.Entities;
using LosPollos.Application.Commands.Restaurants.CreateCommands;
using LosPollos.Application.Commands.Restaurants.UpdateCommands;
namespace LosPollos.Application.Profiles
{
    public  class RestaurantProfile:Profile
    {
        
        public RestaurantProfile( )
        {
            CreateMap<UpdateRestaurantCommand, Resturant>();
            CreateMap<CreateRestaurantCommnad, Resturant>()
                .ForMember(d=>d.Address,option=>option.MapFrom(source=> new Address()
                {
                    City = source.City, 
                    PostalCode = source.PostalCode,     
                    Street  = source.Street
                }));


            CreateMap<Resturant,RestaurantDTO>()
                .ForMember(d=>d.City,option=>option.MapFrom(source=> source.Address == null? null: source.Address.City))
                .ForMember(d => d.Street, option => option.MapFrom(source => source.Address == null ? null : source.Address.Street))
                .ForMember(d => d.PostalCode, option => option.MapFrom(source => source.Address == null ? null : source.Address.PostalCode))
                .ForMember(d=>d.Dishes,option=>option.MapFrom(source=>source.Dishes));
                
        }
    }
}

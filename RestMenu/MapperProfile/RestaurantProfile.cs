using AutoMapper;
using RestMenu.Models;
using RestMenu.Models.ResDTO;

namespace RestMenu.MapperProfile
{
    public class RestaurantProfile:Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant,RestaurantResDTO>();
        }
    }
}

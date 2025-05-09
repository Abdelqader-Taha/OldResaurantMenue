using AutoMapper;
using RestMenu.Models;
using RestMenu.Models.ResDTO;

namespace RestMenu.MapperProfile
{
    public class MenuItemProfile:Profile
    {
        public MenuItemProfile()
        {
            CreateMap<MenuItem,MenuItemResDTO>();
        }
    }
}

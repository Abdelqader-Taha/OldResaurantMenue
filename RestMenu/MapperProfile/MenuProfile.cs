using AutoMapper;
using RestMenu.Models;
using RestMenu.Models.ResDTO;

namespace RestMenu.MapperProfile
{
    public class MenuProfile:Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu,MenuResDTO>();
        }
    }
}

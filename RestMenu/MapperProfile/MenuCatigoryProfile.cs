using AutoMapper;
using RestMenu.Models;
using RestMenu.Models.ResDTO;

namespace RestMenu.MapperProfile
{
    public class MenuCatigoryProfile:Profile
    {
        public MenuCatigoryProfile()
        {
            CreateMap<MenuCatigory,MenuCatigoryResDTO>();
        }
    }
}

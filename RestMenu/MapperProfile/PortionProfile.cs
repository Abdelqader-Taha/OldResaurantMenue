using AutoMapper;
using RestMenu.Models;
using RestMenu.Models.ResDTO;

namespace RestMenu.MapperProfile
{
    public class PortionProfile:Profile
    {

        public PortionProfile()
        {
            CreateMap<Portion,PortionResDTO>();
        }
    }
}

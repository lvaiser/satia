using AutoMapper;
using LoginPoC.Model.User;
using LoginPoC.Web.Areas.Common.Models;

namespace LoginPoC.Web.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            this.CreateMap<Country, CountryViewModel>()
                .ReverseMap();

            this.CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(dest => dest.Country, 
                            opt => {
                                opt.PreCondition(u => u.Country != null);
                                opt.ResolveUsing(u => u.Country.Name);
                            })
                .ReverseMap();
        }
    }
}
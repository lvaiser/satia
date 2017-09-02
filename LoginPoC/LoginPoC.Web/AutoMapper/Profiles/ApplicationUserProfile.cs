using AutoMapper;
using LoginPoC.Model.User;
using LoginPoC.Web.Areas.Security.Models;

namespace LoginPoC.Web.AutoMapper.Profiles
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, RegisterViewModel>()
                .ReverseMap()
                .ForMember(u => u.UserName, opt => opt.MapFrom(vm => vm.Email));

            CreateMap<ApplicationUser, ExternalLoginConfirmationViewModel>()
                .ReverseMap()
                .ForMember(u => u.UserName, opt => opt.ResolveUsing(vm => vm.Email));
        }
    }
}
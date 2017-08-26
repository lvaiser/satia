using AutoMapper;
using LoginPoC.Model.User;
using LoginPoC.Web.Areas.Security.Models;

namespace LoginPoC.Web.AutoMapper.Profiles
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, RegisterViewModel>().ReverseMap();
            CreateMap<ApplicationUser, ExternalLoginConfirmationViewModel>().ReverseMap();
        }
    }
}
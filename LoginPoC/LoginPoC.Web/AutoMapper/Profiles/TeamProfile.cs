using AutoMapper;
using LoginPoC.Model.Teams;
using LoginPoC.Web.Areas.Admin.Models;

namespace LoginPoC.Web.AutoMapper.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            this.CreateMap<Team, TeamViewModel>()
                .ReverseMap();
        }
    }
}
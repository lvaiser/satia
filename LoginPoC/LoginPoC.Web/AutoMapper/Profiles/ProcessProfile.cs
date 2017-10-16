using AutoMapper;
using LoginPoC.Model.Process;
using LoginPoC.Model.User;
using LoginPoC.Web.Areas.Common.Models;
using System.Collections.Generic;
using System.Web;

namespace LoginPoC.Web.AutoMapper.Profiles
{
    public class ProcessProfile : Profile
    {
        public ProcessProfile()
        {
            CreateMap<Model.Process.Process, ProcessViewModel>()
                .ReverseMap();

            CreateMap<ProcessField, ProcessFieldViewModel>()
                .ForMember(vm => vm.Type, opt => opt.MapFrom(x => x.Type.ToString()))
                .ForMember(vm => vm.Value, opt => opt.MapFrom(x => HttpContext.Current.Server.HtmlEncode(x.Value)))
                .ReverseMap();

            CreateMap<Country, KeyValuePair<int, string>>()
                .ConstructUsing(c => new KeyValuePair<int, string>(c.Id, HttpContext.Current.Server.HtmlEncode(c.Name)));
        }
    }
}
using LoginPoC.Model.ProcessType;
using LoginPoC.Web.Areas.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using LoginPoC.Model.User;
using LoginPoC.Model.Process;

namespace LoginPoC.Web.AutoMapper.Profiles
{
    public class ProcessProfile : Profile
    {
        public ProcessProfile()
        {
            CreateMap<Model.Process.Process, ProcessViewModel>()
                .ReverseMap();

            CreateMap<ProcessField, ProcessFieldViewModel>()
                .ReverseMap();

            CreateMap<ProcessTypeField, ProcessTypeFieldViewModel>()
                .ForMember(vm => vm.Type, opt => opt.MapFrom(x => x.Type.ToString()))
                .ReverseMap();

            CreateMap<Country, KeyValuePair<int, string>>()
                .ForMember(x => x.Key, opt => opt.MapFrom(c => c.Id))
                .ForMember(x => x.Value, opt => opt.MapFrom(c => c.Name));
        }
    }
}
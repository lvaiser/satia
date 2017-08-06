using AutoMapper;
using LoginPoC.Web.AutoMapper.Profiles;

namespace LoginPoC.Web.AutoMapper
{
    public static class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new ApplicationUserProfile());
            });
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using LoginPoC.Core.User;
using LoginPoC.Web.Areas.Common.Models;
using LoginPoC.Web.Helpers;
using Microsoft.AspNet.Identity;

namespace LoginPoC.Web.Areas.Common.Controllers
{
    [RequireHttps]
    public class CountryController : Controller
    {
        private ICountryService countryService;
        private IMapper mapper;

        public CountryController(ICountryService countryService, IMapper mapper)
        {
            this.countryService = countryService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> ListAll()
        {
            var countries = await countryService.GetCountriesAsync();
            return this.JsonNet(mapper.Map<List<CountryViewModel>>(countries));
        }
    }
}
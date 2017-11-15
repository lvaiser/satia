using LoginPoC.DAL;
using LoginPoC.Model.User;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace LoginPoC.Core.User
{
    public class EfCountryService : ICountryService
    {
        private ApplicationDbContext Context { get; set; }

        public EfCountryService(ApplicationDbContext context)
        {
            this.Context = context;
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            return await this.Context.Countries.OrderBy(c => c.Name)
                                               .ToListAsync();
        }

        public Country GetById(int countryId)
        {
            return this.Context.Countries.SingleOrDefault(c => c.Id == countryId);
        }
    }
}

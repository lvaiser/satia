using LoginPoC.DAL;
using LoginPoC.Model.User;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
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
            return await this.Context.Countries.ToListAsync();
        }
    }
}

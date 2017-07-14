using LoginPoC.DAL;
using LoginPoC.Model.User;
using System.Collections.Generic;
using System.Linq;

namespace LoginPoC.Core.User
{
    public class EfUserService : IUserService
    {
        private ApplicationDbContext Context { get; set; }

        public EfUserService(ApplicationDbContext context)
        {
            this.Context = context;
        }

        IEnumerable<Country> IUserService.GetCountries()
        {
            return this.Context.Countries.ToList();
        }
    }
}

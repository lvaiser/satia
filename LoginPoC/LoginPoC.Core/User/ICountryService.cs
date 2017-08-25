using LoginPoC.Model.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginPoC.Core.User
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetCountriesAsync();
    }
}

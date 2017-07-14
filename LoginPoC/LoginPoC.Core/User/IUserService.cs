using LoginPoC.Model.User;
using System.Collections.Generic;

namespace LoginPoC.Core.User
{
    public interface IUserService
    {
        IEnumerable<Country> GetCountries();
    }
}

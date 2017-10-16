using LoginPoC.Model.Teams;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginPoC.Core.Teams
{
    public interface ITeamService : IGenericCrudService<Team>
    {
        Task<IEnumerable<Team>> SearchAsync(string name);
    }
}

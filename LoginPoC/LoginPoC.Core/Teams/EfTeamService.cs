using LoginPoC.DAL;
using LoginPoC.Model.Teams;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace LoginPoC.Core.Teams
{
    public class EfTeamService : EfGenericCrudService<Team>, ITeamService
    {
        public EfTeamService(ApplicationDbContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Team>> SearchAsync(string name)
        {
            IQueryable<Team> query = this.context.Teams;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(t => t.Name.Contains(name));
            }

            return await query.ToListAsync();
        }

        public override Team GetById(int id)
        {
            return this.context.Teams.Include(t => t.Users)
                                     .FirstOrDefault(t => t.Id == id);
        }
    }
}

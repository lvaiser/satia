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

        public override void Add(Team entity)
        {
            this.AttachUsers(entity);
            base.Add(entity);
        }

        public override void Update(Team entityToUpdate)
        {
            this.AttachUsers(entityToUpdate);

            var team = this.GetById(entityToUpdate.Id);
            if (team == null)
            {
                throw new System.Exception("No se encontro el equipo especificado");
            }

            team.Users.Clear();
            team.Users.AddRange(entityToUpdate.Users);
            team.Name = entityToUpdate.Name;

            this.context.SaveChanges();
        }

        private void AttachUsers(Team team)
        {
            foreach (var user in team.Users)
            {
                if (this.context.Entry(user).State == EntityState.Detached)
                {
                    this.context.Users.Attach(user);
                }
            }
        }
    }
}

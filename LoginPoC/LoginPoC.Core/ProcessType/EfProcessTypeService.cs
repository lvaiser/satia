using LoginPoC.Core.User;
using LoginPoC.DAL;
using LoginPoC.Model.ProcessType;
using LoginPoC.Model.User;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace LoginPoC.Core.ProcessType
{
    public class EfProcessTypeService : EfGenericCrudService<Model.ProcessType.ProcessType>, IProcessTypeService
    {
        private ApplicationUserManager UserManager { get; set; }

        public EfProcessTypeService(
            ApplicationDbContext context,
            ApplicationUserManager userManager) : base(context)
        {
            this.UserManager = userManager;
        }

        public async Task<IEnumerable<Model.ProcessType.ProcessType>> SearchAsync(string name, string userId)
        {
            var query = dbSet.Where(pt => pt.IsActive).AsQueryable();

            if (await this.UserManager.IsInRoleAsync(userId, ApplicationUserRoles.User))
            {
                query = query.Where(pt => pt.IsAvailable);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(pt => pt.Name.Contains(name));
            }

            return await query.OrderBy(p => p.Name)
                              .ToListAsync();
        }

        public override Model.ProcessType.ProcessType GetById(int id)
        {
            return dbSet.Include(pt => pt.Fields)
                        .Include(pt => pt.Documents)
                        .FirstOrDefault(pt => pt.Id == id);
        }

        public override void Add(Model.ProcessType.ProcessType entityToAdd)
        {
            foreach (ProcessTypeField field in entityToAdd.Fields)
            {
                field.ProcessType = entityToAdd;
                context.ProcessTypeFields.Add(field);
            }

            foreach (ProcessTypeDocument document in entityToAdd.Documents)
            {
                document.ProcessType = entityToAdd;
                context.ProcessTypeDocuments.Add(document);
            }

            entityToAdd.IsActive = true;
            base.Add(entityToAdd);
        }

        public override void Update(Model.ProcessType.ProcessType entityToUpdate)
        {
            var currentFields = context.ProcessTypeFields.Where(p => p.ProcessType.Id == entityToUpdate.Id);

            foreach (ProcessTypeField field in currentFields)
                context.ProcessTypeFields.Remove(field);

            foreach (ProcessTypeField field in entityToUpdate.Fields)
            {
                field.ProcessType = entityToUpdate;
                context.ProcessTypeFields.Add(field);
            }

            var currentDocuments = context.ProcessTypeDocuments.Where(p => p.ProcessType.Id == entityToUpdate.Id);

            foreach (ProcessTypeDocument document in currentDocuments)
                context.ProcessTypeDocuments.Remove(document);

            foreach (ProcessTypeDocument document in entityToUpdate.Documents)
            {
                document.ProcessType = entityToUpdate;
                context.ProcessTypeDocuments.Add(document);
            }

            base.Update(entityToUpdate);
        }

        public override void Delete(int id)
        {
            var processType = this.GetById(id);
            Delete(processType);
        }

        public override void Delete(Model.ProcessType.ProcessType entityToDelete)
        {
            entityToDelete.IsActive = false;
            this.Update(entityToDelete);
        }
    }
}

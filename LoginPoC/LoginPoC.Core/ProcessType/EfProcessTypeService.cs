using LoginPoC.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using LoginPoC.Model.ProcessType;
using System.Threading.Tasks;
using System.Data.Entity;

namespace LoginPoC.Core.ProcessType
{
    public class EfProcessTypeService : EfGenericCrudService<Model.ProcessType.ProcessType>, IProcessTypeService
    {
        public EfProcessTypeService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Model.ProcessType.ProcessType>> SearchAsync(string name)
        {
            var query = dbSet.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(pt => pt.Name.Contains(name));
            }

            return await query.ToListAsync();
        }

        public virtual Model.ProcessType.ProcessType GetById(int id)
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
    }
}

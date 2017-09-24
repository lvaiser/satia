﻿using LoginPoC.DAL;
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
                        .FirstOrDefault(pt => pt.Id == id);
        }

        public override void Add(Model.ProcessType.ProcessType entityToAdd)
        {
            foreach (ProcessTypeField field in entityToAdd.Fields)
            {
                field.ProcessType = entityToAdd;
                context.ProcessTypeFields.Add(field);
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

            base.Update(entityToUpdate);
        }
    }
}

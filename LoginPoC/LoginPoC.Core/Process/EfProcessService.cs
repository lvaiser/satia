﻿using LoginPoC.Core.ProcessType;
using LoginPoC.Core.User;
using LoginPoC.DAL;
using LoginPoC.Model.Process;
using LoginPoC.Model.ProcessType;
using LoginPoC.Model.User;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace LoginPoC.Core.Process
{
    public class EfProcessService : EfGenericCrudService<Model.Process.Process>, IProcessService
    {
        private IProcessTypeService ProcessTypeService { get; set; }
        private ApplicationUserManager UserManager { get; set; }

        public EfProcessService(
            ApplicationDbContext context, 
            IProcessTypeService processTypeService,
            ApplicationUserManager userManager) : base(context)
        {
            this.ProcessTypeService = processTypeService;
            this.UserManager = userManager;
        }

        public async Task<IEnumerable<Model.Process.Process>> SearchAsync(string name)
        {
            var query = dbSet.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => p.Type.Name.Contains(name));
            }

            return await query.ToListAsync();
        }

        public override Model.Process.Process GetById(int id)
        {
            return dbSet.Include(p => p.Fields)
                        .Include(p => p.Fields.Select(f => f.Type))
                        .Include(p => p.Type)                       
                        .FirstOrDefault(p => p.Id == id);
        }

        public override void Add(Model.Process.Process entityToAdd)
        {
            foreach (ProcessField field in entityToAdd.Fields)
            {
                field.Process = entityToAdd;
                field.Type = context.ProcessTypeFields.First(ptf => ptf.Id == field.Type.Id);
                context.ProcessFields.Add(field);
            }

            entityToAdd.Type = context.ProcessTypes.First(pt => pt.Id == entityToAdd.Type.Id);

            base.Add(entityToAdd);
        }

        public override void Update(Model.Process.Process entityToUpdate)
        {
            var currentFields = context.ProcessFields.Where(p => p.Process.Id == entityToUpdate.Id);

            foreach (ProcessField field in currentFields)
                context.ProcessFields.Remove(field);

            foreach (ProcessField field in entityToUpdate.Fields)
            {
                field.Process = entityToUpdate;
                field.Type = context.ProcessTypeFields.First(ptf => ptf.Id == field.Type.Id);
                context.ProcessFields.Add(field);
            }

            base.Update(entityToUpdate);
        }

        public async Task<Model.Process.Process> GetByTypeAsync(int processTypeId, string userId)
        {
            Model.Process.Process process = new Model.Process.Process();

            var user = await this.UserManager.FindByIdAsync(userId);
            Model.ProcessType.ProcessType type = this.ProcessTypeService.GetById(processTypeId);
            process.Type = type;

            foreach (ProcessTypeField ptField in type.Fields)
            {
                ProcessField field = new ProcessField
                {                    
                    Type = ptField,
                    Value = user.GetType().GetProperty(ptField.Type.ToString()).GetValue(user, null)
                };
            }

            return process;
        }
    }
}

using LoginPoC.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using LoginPoC.Model.ProcessType;

namespace LoginPoC.Core.ProcessType
{
    public class EfProcessTypeService : EfGenericCrudService<Model.ProcessType.ProcessType>, IProcessTypeService
    {
        public EfProcessTypeService(ApplicationDbContext context) : base(context)
        {
        }

        public override void Update(Model.ProcessType.ProcessType entityToUpdate)
        {
            var currentFields = context.ProcessTypeFields.Where(p => p.ProcessType.Id == entityToUpdate.Id);

            foreach (ProcessTypeField field in currentFields)
                context.ProcessTypeFields.Remove(field);

            foreach (ProcessTypeField field in entityToUpdate.Fields)
                context.ProcessTypeFields.Add(field);
            
            base.Update(entityToUpdate);
        }
    }
}

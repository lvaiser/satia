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
    }
}

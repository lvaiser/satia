using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginPoC.Core.ProcessType
{
    public interface IProcessTypeService : IGenericCrudService<Model.ProcessType.ProcessType>
    {
        Task<IEnumerable<Model.ProcessType.ProcessType>> SearchAsync(string name, string userId);
    }
}

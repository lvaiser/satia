using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginPoC.Core.Process
{
    public interface IProcessService : IGenericCrudService<Model.Process.Process>
    {
        Task<IEnumerable<Model.Process.Process>> SearchAsync(string name);

        Task<Model.Process.Process> GetByTypeAsync(int processTypeId);
    }    
}

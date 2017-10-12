using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginPoC.Core.Process
{
    public interface IProcessService : IGenericCrudService<Model.Process.Process>
    {
        Task<IEnumerable<Model.Process.Process>> SearchAsync(string name);

        Task<IEnumerable<Model.Process.Process>> SearchMyProcessesAsync(string name, string userId);

        Task<Model.Process.Process> GetByTypeAsync(int processTypeId, string userId);

        Task AssignAgentAsync(int processId, string userId);

        void Add(Model.Process.Process entityToAdd, string creatorUserId);
    }
}

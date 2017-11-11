using LoginPoC.Model.Process;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginPoC.Core.Process
{
	public interface IProcessService : IGenericCrudService<Model.Process.Process>
	{
		Task<IEnumerable<Model.Process.Process>> SearchNotDraftAsync(string name);

		Task<IEnumerable<Model.Process.Process>> SearchMyProcessesAsync(string name, string userId);

		Task<Model.Process.Process> GetByTypeAsync(int processTypeId, string userId);

		Task AssignAgentAsync(int processId, string userId);

		void Add(Model.Process.Process entityToAdd, string creatorUserId);

		Task DeassignAsync(int processId);

		Task ChangeStatusAsync(int processId, ProcessStatus status, string reviewNotes = null);

        Task ArchiveProcessesInProgressAsync(string userId);
    }
}

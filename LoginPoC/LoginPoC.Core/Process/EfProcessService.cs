using LoginPoC.Core.ProcessType;
using LoginPoC.Core.User;
using LoginPoC.DAL;
using LoginPoC.Model.Process;
using LoginPoC.Model.ProcessType;
using LoginPoC.Model.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
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

		public async Task<IEnumerable<Model.Process.Process>> SearchNotDraftAsync(string name)
		{
			var query = dbSet.Where(p => p.Status != ProcessStatus.Draft);
			if (!string.IsNullOrWhiteSpace(name))
			{
				query = query.Where(p => p.Type.Name.Contains(name));
			}

			return await query.ToListAsync();
		}

		public async Task<IEnumerable<Model.Process.Process>> SearchMyProcessesAsync(string name, string userId)
		{
			var query = dbSet.Include(p => p.Fields)
						.Include(p => p.Documents)
						.Include(p => p.Type)
						.Where(p => p.Creator.Id == userId).AsQueryable();

			if (!string.IsNullOrWhiteSpace(name))
			{
				query = query.Where(p => p.Type.Name.Contains(name));
			}

			return await query.ToListAsync();
		}

		public override Model.Process.Process GetById(int id)
		{
			return dbSet.Include(p => p.Fields)
						.Include(p => p.Documents)
						.Include(p => p.Type)                       
						.FirstOrDefault(p => p.Id == id);
		}

		public void Add(Model.Process.Process entityToAdd, string creatorUserId)
		{
			var user = this.UserManager.FindByIdAsync(creatorUserId).Result;
			entityToAdd.Creator = user;

			this.Add(entityToAdd);
		}

		public override void Add(Model.Process.Process entityToAdd)
		{
			foreach (ProcessField field in entityToAdd.Fields)
			{
				field.ProcessId = entityToAdd.Id;
				field.Process = null;               
				context.ProcessFields.Add(field);
			}

			foreach (ProcessDocument document in entityToAdd.Documents)
			{
				document.ProcessId = entityToAdd.Id;
				document.Process = null;
				context.ProcessDocuments.Add(document);
			}

			entityToAdd.TypeId = entityToAdd.Type.Id;
			entityToAdd.Type = null;
			entityToAdd.CreationDate = DateTime.Now;

			base.Add(entityToAdd);
		}

		public override void Update(Model.Process.Process entityToUpdate)
		{
			var currentFields = context.ProcessFields.Where(p => p.Process.Id == entityToUpdate.Id);

			foreach (ProcessField field in currentFields)
				context.ProcessFields.Remove(field);

			foreach (ProcessField field in entityToUpdate.Fields)
			{
				field.ProcessId = entityToUpdate.Id;
				context.ProcessFields.Add(field);
			}

			var currentDocuments = context.ProcessDocuments.Where(p => p.Process.Id == entityToUpdate.Id);

			foreach (ProcessDocument document in currentDocuments)
				context.ProcessDocuments.Remove(document);

			foreach (ProcessDocument document in entityToUpdate.Documents)
			{
				document.ProcessId = entityToUpdate.Id;
				context.ProcessDocuments.Add(document);
			}

			base.Update(entityToUpdate);
		}

		public async Task AssignAgentAsync(int processId, string userId)
		{
			if (await this.UserManager.IsInRoleAsync(userId, ApplicationUserRoles.Agent))
			{
				var process = await this.context.Processes.FirstOrDefaultAsync(p => p.Id == processId);
				process.AssignedAgentId = userId;
				
				await this.context.SaveChangesAsync();
			}
			else
			{
				throw new ApplicationException("User is not an agent.");
			}
		}

		public async Task DeassignAsync(int processId)
		{
			var process = await this.context.Processes.FirstOrDefaultAsync(p => p.Id == processId);
			process.AssignedAgentId = null;

			await this.context.SaveChangesAsync();
		}

		public async Task ChangeStatusAsync(int processId, ProcessStatus status, string reviewNotes = null)
		{
			var process = await this.context.Processes.FirstOrDefaultAsync(p => p.Id == processId);
			process.Status = status;

			process.ReviewNotes = null;
			if (status == ProcessStatus.Approved || status == ProcessStatus.Rejected)
			{
				process.ReviewDate = DateTime.Now;
				process.ReviewNotes = reviewNotes;
			}
			
			await this.context.SaveChangesAsync();
		}

		public async Task<Model.Process.Process> GetByTypeAsync(int processTypeId, string userId)
		{
			Model.Process.Process process = new Model.Process.Process();

			var user = await this.UserManager.FindByIdAsync(userId);
			Model.ProcessType.ProcessType type = this.ProcessTypeService.GetById(processTypeId);
			process.Type = type;

			List<ProcessField> fields = new List<ProcessField>();
			foreach (ProcessTypeField ptField in type.Fields)
			{
				object value = null;
				PropertyInfo property = user.GetType().GetProperty(ptField.Type.ToString());
				if (property != null)
				{
					value = property.GetValue(user);

                    switch (ptField.Type)
                    {
                        case FieldType.Date:
                        case FieldType.BirthDate:
                            value = ((DateTime)value).ToString(@"dd\/MM\/yyyy");
                            break;
                        default:
                            break;
                    }
                }

                ProcessField field = new ProcessField
				{
					Name = ptField.Name,
					IsRequired = ptField.IsRequired,
					Type = ptField.Type,
					Value = (value == null ? string.Empty : value.ToString())
				};

				fields.Add(field);
			}

			process.Fields = fields;

			List<ProcessDocument> documents = new List<ProcessDocument>();
			foreach (ProcessTypeDocument ptDocument in type.Documents)
			{
				ProcessDocument document = new ProcessDocument
				{
					Name = ptDocument.Name,
					IsRequired = ptDocument.IsRequired,
					IsAvailable = false
				};

				documents.Add(document);
			}

			process.Documents = documents;

			return process;
		}

        public async Task ArchiveProcessesInProgressAsync(string userId)
        {
            var processes = this.context.Processes.Where(
                p => p.CreatorId == userId && 
                (p.Status == ProcessStatus.Draft || p.Status == ProcessStatus.Submitted));

            await processes.ForEachAsync(p => p.Status = ProcessStatus.Archived);

            await this.context.SaveChangesAsync();
        }
    }
}

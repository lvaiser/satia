using LoginPoC.DAL;
using System;
using System.Threading.Tasks;
using System.Data.Entity;
using System.IO;

namespace LoginPoC.Core.File
{
    public class EfFileService : IFileService
    {
        private ApplicationDbContext context;
        private ApplicationSettings settings;

        public EfFileService(ApplicationDbContext context, ApplicationSettings settings)
        {
            this.context = context;
            this.settings = settings;

            if (!Directory.Exists(settings.FilesBasePath))
            {
                Directory.CreateDirectory(settings.FilesBasePath);
            }
        }

        public async Task<Model.File.File> GetByAsync(int Id)
        {
            return await context.Files.FirstOrDefaultAsync(f => f.Id == Id);
        }

        public async Task<Model.File.File> GetByNameAsync(string name)
        {
            return await context.Files.FirstOrDefaultAsync(f => f.FileName == name);
        }

        public async Task<Model.File.File> CreateAsync(Model.File.File file)
        {
            var entity = context.Files.Add(file);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var file = await this.GetByAsync(id);
            if (file == null)
            {
                return;
            }

            context.Files.Remove(file);

            await context.SaveChangesAsync();

            if (System.IO.File.Exists(settings.FilesBasePath + file.FileName))
            {
                System.IO.File.Delete(settings.FilesBasePath + file.FileName);
            }
        }

        public async Task<string> StoreAsync(System.IO.Stream fileStream, string extension)
        {
            var fileName = Guid.NewGuid().ToString() + extension;

            using (var newFileStream = System.IO.File.OpenWrite(settings.FilesBasePath + fileName))
            {
                await fileStream.CopyToAsync(newFileStream);
                await newFileStream.FlushAsync();
            }

            return fileName;
        }

        public async Task<FileStream> ReadAsync(int id)
        {
            var file = await this.GetByAsync(id);

            if (file == null || !System.IO.File.Exists(settings.FilesBasePath + file.Path))
            {
                return null;
            }

            return System.IO.File.OpenRead(settings.FilesBasePath + file.Path);
        }
    }
}

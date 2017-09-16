using System.IO;
using System.Threading.Tasks;

namespace LoginPoC.Core.File
{
    public interface IFileService
    {
        Task<Model.File.File> GetByAsync(int Id);

        Task<Model.File.File> GetByNameAsync(string name);

        Task<Model.File.File> CreateAsync(Model.File.File file);

        Task DeleteAsync(int id);

        Task<string> StoreAsync(System.IO.Stream fileStream, string extension);

        Task<FileStream> ReadAsync(int id);
    }
}

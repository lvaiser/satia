using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using LoginPoC.Core;
using LoginPoC.Core.File;
using LoginPoC.Model.File;
using LoginPoC.Web.Helpers;

namespace LoginPoC.Web.Areas.Common.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileService fileService;
        private readonly IMapper mapper;

        public FileController(IFileService fileService, IMapper mapper)
        {
            this.fileService = fileService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Download(int id)
        {
            var file = await this.fileService.GetByAsync(id);
            var stream = await this.fileService.ReadAsync(id);

            if (stream == null)
            {
                return this.HttpNotFound();
            }

            return File(stream, file.ContentType, this.SanitizeFilename(file.DisplayName) + this.ExtractExtension(file.FileName));
        }

        [HttpPost]
        public async Task<ActionResult> Upload(string displayName)
        {
            var file = this.Request.Files[0];
            using (var fileStream = file.InputStream)
            {
                var fileName = await fileService.StoreAsync(fileStream, this.ExtractExtension(file.FileName));

                var newFile = new File()
                {
                    ContentType = file.ContentType,
                    FileName = fileName,
                    Path = fileName,
                    DisplayName = displayName
                };

                newFile = await fileService.CreateAsync(newFile);

                return this.JsonNet(newFile);
            }
        }

        [HttpDelete]
        public async Task DeleteFile(int id)
        {
            await this.fileService.DeleteAsync(id);
        }

        private string SanitizeFilename(string fileName)
        {
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c, '_');
            }

            return fileName;
        }

        private string ExtractExtension(string fileName)
        {
            var match = Regex.Match(fileName, @"\.\w+$");

            return match?.Value ?? "";
        }
    }
}
using LoginPoC.DAL;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LoginPoC.Core
{
    public class ApplicationSettings
    {
        public string FilesBasePath { get; set; }

        public string MailAccount { get; set; }

        public string MailPassword { get; set; }
    }
}

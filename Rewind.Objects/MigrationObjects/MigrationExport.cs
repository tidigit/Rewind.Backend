using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Objects.MigrationObjects
{
    public class MigrationExport: IMigrationExport
    {
        public FileType FileType { get; set; }
        public int  ExportVersion { get; set; }
        public string Schema { get; set; }
        public string Data { get; set; }
    }

    public enum FileType
    {
        JSON = 1,
        CSV = 2,
        PDF = 3
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Objects
{
    public class MigrationApplication: ThirdPartyApplication
    {
        public List<IMigrationExport> MigrationExports { get; set; }
        public List<MigrationGuide> MigrationGuides { get; set; }
    }
}

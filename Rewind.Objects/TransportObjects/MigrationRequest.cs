using Rewind.Objects.MigrationObjects.DayOne;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Objects.TransportObjects
{
    public class MigrationRequest: BaseRequest
    {
        public int UserId { get; set; }
        public IMigrationExport MigrationExport { get; set; }
        public ThirdPartyApplicationCode ThirdPartyApplication { get; set; }
    }
}

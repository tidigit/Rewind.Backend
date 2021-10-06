using Rewind.Objects.MigrationObjects.DayOne;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Objects.TransportObjects
{
    public class DayOneMigrationRequest: BaseRequest
    {
        public int UserId { get; set; }
        public DayOneJsonExport DayOneJsonExport { get; set; }
    }
}

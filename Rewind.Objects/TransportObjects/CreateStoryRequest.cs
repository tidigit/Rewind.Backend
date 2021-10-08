using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Objects.TransportObjects
{
    public class CreateStoryRequest: BaseRequest
    {
        public List<Story> StoriesToCreate { get; set; }
    }
}

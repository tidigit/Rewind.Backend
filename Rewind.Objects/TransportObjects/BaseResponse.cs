using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Objects.TransportObjects
{
    public class BaseResponse
    {
        public int Code { get; set; }
        public int SubCode { get; set; }
        public string Description { get; set; }
    }
}

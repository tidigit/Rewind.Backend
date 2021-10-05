using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Objects.TransportObjects
{
    public enum LoginType : short
    {
        GoogleSignOn = 4,
        AppleSignOn = 3,
        Phone = 2,
        Email = 1
    }

    class TransportFlags
    {
    }
}

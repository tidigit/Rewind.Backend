using Rewind.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Utilities
{
    public interface IJwtHelper
    {
        string BuildToken(Account user);
    }
}

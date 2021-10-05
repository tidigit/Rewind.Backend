using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Objects.TransportObjects
{
    public class LoginRequest: BaseRequest
    {
        public User User { get; set; }
        public LoginType LoginType { get; set; }
    }
    public class SignupRequest: BaseRequest
    {
        public User User { get; set; }
    }


}

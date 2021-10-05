using Rewind.Objects.TransportObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Objects.TransportObjects
{

    public class AccountResponse: BaseResponse
    {
        public string AccessToken { get; set; }
        public User User { get; set; }
    }

    public class LoginResponse: AccountResponse
    {

    }
    public class SignupResponse: AccountResponse
    { 


    }


}

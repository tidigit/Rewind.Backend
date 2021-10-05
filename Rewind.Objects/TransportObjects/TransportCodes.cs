using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Objects.TransportObjects
{
    public class GeneralCodes
    {
        public const int TransactionSuccessful = 1000;
        public const int InsufficentData = 1001;
        public const int SomethingWentWrong = 1002;
    }
    public class LoginCodes : GeneralCodes
    {
        public const int UserDoesnotExist = 2000;
        public const int WrongPassword = 2001;
    }
    public class SignupCodes: GeneralCodes
    {
        public const int SignupSuccessful = 2100;
        public const int UserAlreadyExists = 2101;
        
    }


}

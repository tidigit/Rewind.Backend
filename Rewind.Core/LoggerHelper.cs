using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Utilities
{
    public class LoggerHelper
    {



        public static void LogError(Exception exception)
        {
            //todo logging
            Console.WriteLine(exception);
        }
    }
}

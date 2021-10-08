using Rewind.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Utilities
{
    public class AccountHelper
    {
        public static User MapUserFromAccount(Account userAccount)
        {
            var user = new User()
            {
                UserProfile = new UserProfile()
                {
                    FirstName = userAccount.FirstName,
                    LastName = userAccount.LastName,
                    Phone = userAccount.Phone,
                    Email = userAccount.Email
                }
            };
            return user;
        }
    }
}

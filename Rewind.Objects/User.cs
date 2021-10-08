using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Objects
{
    public class User
    {
        public ObjectId Id { get; set; }
        public UserProfile UserProfile { get; set; }
        public UserSettings UserSettings { get; set; }

    }

    public class UserSettings
    {
        public string UserTimeZone { get; set; }
    }

    public class UserProfile : HumanProfile
    {
        public string ProfilePhoto { get; set; }
    }
}

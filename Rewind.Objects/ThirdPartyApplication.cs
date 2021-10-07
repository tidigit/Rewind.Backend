using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Objects
{
    public class ThirdPartyApplication
    {
        public string Name { get; set; }
        public ObjectId Id { get; set; }
        public ThirdPartyApplicationCode ThirdPartyApplicationCode { get; set; }
    }
    public enum ThirdPartyApplicationCode
    {
        DayOne = 1,
        Journey = 2,
        Momento = 3,
        Dairly = 4

    }
}

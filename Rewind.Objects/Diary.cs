using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Objects
{
    public class Diary
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public ObjectId ColorId { get; set; }
        public ObjectId CoverId { get; set; }
        public DateTime CreatedTimeStampInUtc { get; set; }
        public DateTime LastModifiedTimeStampInUtc { get; set; }
        public bool IsArchived { get; set; }
        public bool IsHidden { get; set; }
        public bool IsGroupDiary { get; set; }
        public bool IsCurationsEnabled { get; set; }
        public bool IsCollectionsEnabled { get; set; }
        public ObjectId DefaultTemplateId { get; set; }
        public List<ObjectId> Collections { get; set; }
        public List<ObjectId> Curations { get; set; }
        public PartnerSettings PartnerSettings { get; set; }
        public LegacySettings LegacySettings { get; set; }
    }

    public class LegacySettings
    {

    }

    public class PartnerSettings
    {
        public bool IsEnabled { get; set; }
        public List<ObjectId> Contributors { get; set; }
    }

    public class Contributor : Account
    {
        public DiaryAccessType AccessType { get; set; }
    }



    public class AutomationRule
    {
    }

    public enum RewindColorCode
    {

    }
    public enum DiaryAccessType
    {
        ReadOnly = 1,
        ReadWrite = 2
    }
}

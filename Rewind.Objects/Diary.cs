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
        public RewindColorCode Color { get; set; }
        public DateTime CreatedTimeStampInUtc { get; set; }
        public DateTime LastModifiedTimeStampInUtc { get; set; }
        public bool IsArchived { get; set; }
        public bool IsDiarySectionsEnabled { get; set; }
        public ObjectId DefaultTemplateId { get; set; }
        public List<Section> Sections { get; set; }
        public PartnerSettings PartnerSettings { get; set; }
        public LegacySettings LegacySettings { get; set; }


    }

    public class LegacySettings
    {

    }

    public class PartnerSettings
    {
        public bool IsEnabled { get; set; }
        public List<Contributor> Contributors { get; set; }
    }

    public class Contributor : Account
    {
        public DiaryAccessType AccessType { get; set; }
    }

    public class Section
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public List<AutomationRule> AutomationRules { get; set; }
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

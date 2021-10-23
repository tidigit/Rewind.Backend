using System.Collections.Generic;


namespace Rewind.Objects
{
    public class Curation
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public List<AutomationRule> AutomationRules { get; set; }
    }

}
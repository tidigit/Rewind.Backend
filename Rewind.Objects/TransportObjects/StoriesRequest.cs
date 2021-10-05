using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Objects.TransportObjects
{
    public class StoriesRequest: BaseRequest
    {
        public ViewType ViewType { get; set; }
        public Context SearchCriteria { get; set; }
    }

    public class Context
    {
        public List<int> DiaryIdentifiers { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public List<IMention> Mentions { get; set; }
        public List<string> Tags { get; set; }
        public string SearchText { get; set; }
    }

    public enum ViewType
    {
        Timeline = 1,
        Snapshot = 2,
        Gallery = 3
    }
}

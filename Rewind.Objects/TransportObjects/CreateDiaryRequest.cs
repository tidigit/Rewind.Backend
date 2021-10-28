
using System.Collections.Generic;

namespace Rewind.Objects.TransportObjects
{
    public class CreateDiaryRequest : BaseRequest
    {
        public string DiaryName { get; set; }
        public RewindIdentifier ColorId { get; set; }
        public RewindIdentifier CoverId { get; set; }
        public bool IsCurationsEnabled { get; set; }
        public bool IsCollectionsEnabled { get; set; }
        public bool IsGroupDiary { get; set; }

    }
}

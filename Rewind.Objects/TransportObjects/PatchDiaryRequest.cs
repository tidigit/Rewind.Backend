using System.Collections.Generic;

namespace Rewind.Objects.TransportObjects
{
    public class PatchDiaryRequest : BaseRequest
    {
        public RewindResourceIdentifier DiaryId { get; set; }
        public List<Patch> Patches { get; set; }
    }
}

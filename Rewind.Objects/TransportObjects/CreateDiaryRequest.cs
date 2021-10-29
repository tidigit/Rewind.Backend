
namespace Rewind.Objects.TransportObjects
{
    public class CreateDiaryRequest : BaseRequest
    {
        public string DiaryName { get; set; }
        public RewindResourceIdentifier ColorId { get; set; }
        public RewindResourceIdentifier CoverId { get; set; }
        public bool IsCurationsEnabled { get; set; }
        public bool IsCollectionsEnabled { get; set; }
        public bool IsGroupDiary { get; set; }

    }
}

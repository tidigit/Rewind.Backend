
namespace Rewind.Objects.TransportObjects
{
    public class CreateDiaryRequest: BaseRequest
    {
        public string DiaryName { get; set; }
        public string ColorCode { get; set; }
        public string CoverCode { get; set; }
        

    }
}

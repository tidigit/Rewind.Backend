using System.Collections.Generic;


namespace Rewind.Objects.TransportObjects
{
    public class CreateStoryRequest: BaseRequest
    {
        public List<Story> StoriesToCreate { get; set; }
    }
}

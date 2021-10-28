using System.Collections.Generic;

namespace Rewind.Objects.TransportObjects
{
    public class PatchObject
    {
        public List<Patch> Patches
        { get; set; }
    }
    public class Patch
    {
        public string Operation { get; set; }
        public string Value { get; set; }
        public string Field { get; set; }
    }

}
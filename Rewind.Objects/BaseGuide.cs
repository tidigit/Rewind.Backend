using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Objects
{
    public class BaseGuide
    {
        public string Name { get; set; }
        public List<Step> Steps { get; set; }
    }

    public class Step
    {
        public int StepNumber { get; set; }
        public string ImageOrGif { get; set; }
    }
}

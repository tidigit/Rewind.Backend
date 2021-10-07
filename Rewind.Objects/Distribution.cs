using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Objects
{
    public class Distribution
    {
        public DeviceType DeviceType { get; set; }
        public DeviceOperatingSystem DeviceOperatingSystem { get; set; }
        public DeviceModel DeviceModel { get; set; }
        public string OperatingSystemVersion { get; set; }
    }

    public enum DeviceType
    {
        Web = 1,
        Phone = 2,
        Tablet = 3,
        Desktop = 4,
        Television = 5,
        SmartSpeaker = 6,
        SmartWatch = 7
    }

    public enum DeviceOperatingSystem
    {
        IOS = 1,
        Andriod = 2,
        IpadOS = 3,
        MacOS = 4,
        Windows = 5,
        WatchOS = 6,
        AndriodWear = 7,
        Chromium = 8,
        Safari = 9,
        Postman = 10
    }

    public enum DeviceModel
    {
        iPhone12 = 1,
        iPhone11 = 2,
        iPhone10 = 3
    }
}

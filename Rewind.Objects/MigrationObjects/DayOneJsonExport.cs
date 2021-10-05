using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Objects.MigrationObjects.DayOne
{
    public class Metadata
    {
        public string Version { get; set; }
    }

    public class Center
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }

    public class Region
    {
        public Center center { get; set; }
        public string identifier { get; set; }
        public double radius { get; set; }
    }

    public class Location
    {
        public Region region { get; set; }
        public string localityName { get; set; }
        public string country { get; set; }
        public string timeZoneName { get; set; }
        public string administrativeArea { get; set; }
        public double longitude { get; set; }
        public string placeName { get; set; }
        public double latitude { get; set; }
    }

    public class Photo
    {
        public int fileSize { get; set; }
        public int orderInEntry { get; set; }
        public string creationDevice { get; set; }
        public int duration { get; set; }
        public bool favorite { get; set; }
        public string type { get; set; }
        public string filename { get; set; }
        public string identifier { get; set; }
        public DateTime date { get; set; }
        public int height { get; set; }
        public string fnumber { get; set; }
        public int width { get; set; }
        public string md5 { get; set; }
        public bool isSketch { get; set; }
        public string focalLength { get; set; }
        public string lensModel { get; set; }
        public Location location { get; set; }
        public string cameraMake { get; set; }
        public string lensMake { get; set; }
        public int? exposureBiasValue { get; set; }
        public string cameraModel { get; set; }
    }

    public class Weather
    {
        public DateTime sunsetDate { get; set; }
        public string weatherCode { get; set; }
        public string weatherServiceName { get; set; }
        public double temperatureCelsius { get; set; }
        public int windBearing { get; set; }
        public DateTime sunriseDate { get; set; }
        public string conditionsDescription { get; set; }
        public int pressureMB { get; set; }
        public double moonPhase { get; set; }
        public double visibilityKM { get; set; }
        public int relativeHumidity { get; set; }
        public int windSpeedKPH { get; set; }
        public double windChillCelsius { get; set; }
        public string moonPhaseCode { get; set; }
    }

    public class UserActivity
    {
        public string activityName { get; set; }
        public int stepCount { get; set; }
    }

    public class Music
    {
        public int albumYear { get; set; }
        public string album { get; set; }
        public string track { get; set; }
        public string artist { get; set; }
    }

    public class Entry
    {
        public string uuid { get; set; }
        public string timeZone { get; set; }
        public List<Photo> photos { get; set; }
        public DateTime modifiedDate { get; set; }
        public int duration { get; set; }
        public string creationDevice { get; set; }
        public string richText { get; set; }
        public bool starred { get; set; }
        public string creationDeviceType { get; set; }
        public double editingTime { get; set; }
        public bool isPinned { get; set; }
        public List<string> tags { get; set; }
        public bool isAllDay { get; set; }
        public string creationOSVersion { get; set; }
        public DateTime creationDate { get; set; }
        public string creationDeviceModel { get; set; }
        public string creationOSName { get; set; }
        public string text { get; set; }
        public Weather weather { get; set; }
        public Location location { get; set; }
        public UserActivity userActivity { get; set; }
        public Music music { get; set; }
    }

    public class DayOneJsonExport
    {
        public Metadata metadata { get; set; }
        public List<Entry> entries { get; set; }
    }

}

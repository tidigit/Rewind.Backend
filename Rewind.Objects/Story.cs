using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Objects
{
    public class Story
    {
        public ObjectId _id { get; set; }
        public DateTime CreatedTimeStampInUtc { get; set; }
        public DateTime LastModifiedTimeStampInUtc { get; set; }
        public Place Place { get; set; }
        public WeatherStamp WeatherStamp { get; set; }
        public bool IsEventStory { get; set; }
        public Event Event { get; set; }
        public bool IsAutomated { get; set; }
        public bool IsInDraftStage { get; set; }
        public bool IsFavorite { get; set; }
        public int TimeToWriteInSeconds { get; set; }
        public Device CreationDevice { get; set; }
        public string StoryTitle { get; set; }
        public Thumbnail Thumbnail { get; set; }
        public int DiaryIdentifier { get; set; }
        public List<int> DiarySectionIdentifiers { get; set; }
        public List<string> Tags { get; set; }
        public List<IMention> Mentions { get; set; }
        public Body Body { get; set; }
        public Cover Cover { get; set; }

    }

    public class Cover
    {

    }

    public class Body
    {
        public string MarkDownText { get; set; }

    }

    public class Person: IMention
    {
    }

    public class Thumbnail
    {
    }

    public class Device
    {
        public string DeviceName { get; set; }
        public string DeviceOperatingSystem { get; set; }
    }

    public class Event
    {

    }
    public class WeatherStamp
    {
        public double TemperatureInCelsius { get; set; }
        public string Condition { get; set; }
    }

    public class Place: IMention
    {
        public PlaceCoordinates PlaceCoordinates { get; set; }
        public string PlaceName { get; set; }
    }


    public class PlaceCoordinates
    {
        private readonly double latitude;
        private readonly double longitude;

        public double Latitude { get { return latitude; } }
        public double Longitude { get { return longitude; } }

        public PlaceCoordinates(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", Latitude, Longitude);
        }
    }
}

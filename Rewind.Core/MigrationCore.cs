using Microsoft.Extensions.Configuration;
using Rewind.Objects;
using Rewind.Objects.MigrationObjects.DayOne;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Core
{
    public class MigrationCore
    {
        private readonly IConfiguration _config;
        public MigrationCore(IConfiguration configuration)
        {
            _config = configuration;
        }
        public void MapFromDayOneJson(DayOneJsonExport dayOneJsonExport, int userId)
        {
            var stories = new List<Story>();
            foreach (var slot in dayOneJsonExport.entries)
            {
                var story = new Story();
                story.CreatedTimeStampInUtc = slot.creationDate;
                story.CreationDevice = new Device()
                {
                    DeviceName = slot.creationDevice,
                    DeviceOperatingSystem = slot.creationOSName
                };
                story.Tags = slot.tags;
                story.IsFavorite = slot.starred;
                story.Body.MarkDownText = slot.text;
                story.Place.PlaceCoordinates = new PlaceCoordinates(slot.location?.latitude ?? slot.location?.region?.center.Latitude ?? 0,
                    slot.location?.longitude ?? slot.location?.region?.center.Longitude ?? 0); //todo - get saved place from coordinates
                story.WeatherStamp = new WeatherStamp()
                {
                    TemperatureInCelsius = slot.weather?.temperatureCelsius ?? 0,
                    Condition = slot.weather.weatherCode //todo - map from weathercode or conditions description
                };
                stories.Add(story);
            }
            new StoryCore(_config).CreateStories(stories, userId);
        }
    }
}

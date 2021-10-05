using Rewind.Objects;
using Rewind.Objects.MigrationObjects.DayOne;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Core
{
    class MigrationCore
    {

        void MapFromDayOneJson(DayOneJsonExport dayOneJsonExport)
        {
            var epicSchedules = new List<Story>();
            foreach (var slot in dayOneJsonExport.entries)
            {
                var epicSchedule = new Story();
                epicSchedule.CreatedTimeStampInUtc = slot.creationDate;
                epicSchedule.CreationDevice = new Device()
                {
                    DeviceName = slot.creationDevice,
                    DeviceOperatingSystem = slot.creationOSName
                };
                epicSchedule.Tags = slot.tags;
                epicSchedule.IsFavorite = slot.starred;
                epicSchedule.Body.MarkDownText = slot.text;
                epicSchedule.Place.PlaceCoordinates = new PlaceCoordinates(slot.location?.latitude ?? slot.location?.region?.center.Latitude ?? 0,
                    slot.location?.longitude ?? slot.location?.region?.center.Longitude ?? 0); //todo - get saved place from coordinates
                epicSchedule.WeatherStamp = new WeatherStamp()
                {
                    TemperatureInCelsius = slot.weather?.temperatureCelsius ?? 0,
                    Condition = slot.weather.weatherCode //todo - map from weathercode or conditions description
                };
            }
        }
    }
}

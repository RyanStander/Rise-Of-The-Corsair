using System.Linq;
using UnityEngine;

namespace Weather
{
    public static class SeasonalWeatherPicker
    {
        public static WeatherPreset DetermineWeather(WeatherData[] weatherDataSet)
        {
            var count = 0;
            var totalChanceValue =
                weatherDataSet.Sum(springWeather => springWeather.ChanceOfOccurrence);

            var calculatedChance = Random.Range(0, totalChanceValue);

            foreach (var springWeather in weatherDataSet)
            {
                count += springWeather.ChanceOfOccurrence;

                if (count < calculatedChance)
                    continue;

                return springWeather.WeatherPreset;
            }

            return null;
        }
    }
}

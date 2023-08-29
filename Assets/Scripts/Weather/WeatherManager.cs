using System;
using System.Linq;
using TimeAndSeasons;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Weather
{
    public class WeatherManager : MonoBehaviour
    {
        [FormerlySerializedAs("dateCalendar")] [SerializeField]
        private DateCalendarManager dateCalendarManager;

        [SerializeField] private WeatherDataSet weatherDataSet;

        [SerializeField, Tooltip("In minutes")]
        private float minTimeBetweenWeatherChanges = 5f;

        [SerializeField, Tooltip("In minutes")]
        private float maxTimeBetweenWeatherChanges = 15f;

        // Internal variables
        private float timeSinceLastWeatherChange;
        private WeatherPreset currentWeather;

        private void Start()
        {
            timeSinceLastWeatherChange = Random.Range(minTimeBetweenWeatherChanges, maxTimeBetweenWeatherChanges);
            ChangeWeather();
        }

        private void Update()
        {
            timeSinceLastWeatherChange += Time.deltaTime;

            // Check if it's time to change the weather
            if (timeSinceLastWeatherChange >= maxTimeBetweenWeatherChanges)
            {
                timeSinceLastWeatherChange = 0f;
                ChangeWeather();
            }
        }

        private void ChangeWeather()
        {
            currentWeather = dateCalendarManager.currentSeason switch
            {
                Season.Spring => SeasonalWeatherPicker.DetermineWeather(weatherDataSet.SpringWeatherData),
                Season.Summer => SeasonalWeatherPicker.DetermineWeather(weatherDataSet.SummerWeatherData),
                Season.Autumn => SeasonalWeatherPicker.DetermineWeather(weatherDataSet.AutumnWeatherData),
                Season.Winter => SeasonalWeatherPicker.DetermineWeather(weatherDataSet.WinterWeatherData),
                _ => throw new ArgumentOutOfRangeException()
            };

            // Notify event subscribers about the weather change
            //OnWeatherChange?.Invoke(currentWeather);

            // Set a new random time interval for the next weather change
            maxTimeBetweenWeatherChanges = Random.Range(minTimeBetweenWeatherChanges, maxTimeBetweenWeatherChanges);
        }
    }
}

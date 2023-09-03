using UnityEngine;

namespace Weather
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Scriptables/Weather Data Set")]
    public class WeatherDataSet : ScriptableObject
    {
        [Header("Spring")] public WeatherData[] SpringWeatherData;

        [Header("Summer")] public WeatherData[] SummerWeatherData;

        [Header("Autumn")] public WeatherData[] AutumnWeatherData;

        [Header("Winter")] public WeatherData[] WinterWeatherData;
    }

    [System.Serializable]
    public class WeatherData
    {
        public WeatherPreset WeatherPreset;
        [Range(1, 100)] public int ChanceOfOccurrence;
    }
}

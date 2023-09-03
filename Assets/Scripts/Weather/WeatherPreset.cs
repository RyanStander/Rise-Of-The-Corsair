using UnityEngine;

namespace Weather
{
    [System.Serializable]
    [CreateAssetMenu(menuName ="Scriptables/Weather Preset")]
    public class WeatherPreset : ScriptableObject
    {
        public WeatherCondition WeatherCondition;
        public float WindSpeedModifier;
        public float TemperatureModifier;
        public Gradient LightingGradient;
        public float FogDensityModifier;
        public Vector2 DurationRangeInMinutes = new Vector2(1, 10) ;
    }
}

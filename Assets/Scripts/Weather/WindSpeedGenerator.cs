using UnityEngine;

namespace Weather
{
    public static class WindSpeedGenerator
    {
        /// <summary>
        /// Takes the values and generates a wind speed based on the mean speed
        /// </summary>
        /// <param name="minSpeed">the lowest value the wind speed can be</param>
        /// <param name="maxSpeed">the highest value the wind speed can be</param>
        /// <param name="meanSpeed">the most common the wind speed can be</param>
        /// <returns>A value that is usually closer to the mean but could equal close to the min or max</returns>
        public static float GenerateWindSpeed(float minSpeed, float maxSpeed, float meanSpeed)
        {
            var u = Random.value;
            var f = (meanSpeed - minSpeed) / (maxSpeed - minSpeed);

            if (u < f)
            {
                return minSpeed + Mathf.Sqrt(u * (maxSpeed - minSpeed) * (meanSpeed - minSpeed));
            }

            return maxSpeed - Mathf.Sqrt((1 - u) * (maxSpeed - minSpeed) * (maxSpeed - meanSpeed));
        }
    }
}

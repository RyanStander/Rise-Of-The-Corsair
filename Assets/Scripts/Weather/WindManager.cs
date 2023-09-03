using System;
using Events;
using TimeAndSeasons;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Weather
{
    /// <summary>
    /// The WindManager script is responsible for simulating and managing the dynamic wind system in the game.
    /// It determines the wind direction that affects ship movement and navigation as well as water direction.
    /// The script handles generating initial wind direction, gradual changes over time,
    /// and ensuring smooth transitions for a realistic and immersive wind experience.
    /// </summary>
    public class WindManager : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Configurable parameters")] [SerializeField]
        private float minWindSpeed = 15f;

        [SerializeField] private float maxWindSpeed = 120f;
        [SerializeField] private float meanWindSpeed = 50f;
        [SerializeField] private float minWindSpeedChangeRate = 0.1f;
        [SerializeField] private float maxWindSpeedChangeRate = 0.5f;
        [SerializeField] private float minWindDirectionChangeRate = 0.1f;
        [SerializeField] private float maxWindDirectionChangeRate = 0.5f;

        [SerializeField] private float minTimeBetweenWindChanges = 60f;
        [SerializeField] private float maxTimeBetweenWindChanges = 600f;

        #endregion


        #region Private Variables

        private bool started;
        private float windSpeed;
        private float windDirection;
        private float newWindSpeed;
        private float newWindDirection;
        private float windSpeedChangeRate;
        private float windDirectionChangeRate;

        private WeatherPreset currentWeatherPreset;

        private float timeSinceLastWindChange;

        #endregion


        #region On Events

        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventIdentifiers.WeatherHasChanged, OnWeatherHasChanged);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventIdentifiers.WeatherHasChanged, OnWeatherHasChanged);
        }

        private void OnWeatherHasChanged(EventData eventData)
        {
            if (!eventData.IsEventOfType(out WeatherHasChanged weatherHasChanged))
                return;

            currentWeatherPreset = weatherHasChanged.WeatherPreset;

            if (!started)
            {
                started = true;
                SetNewWind();
            }

            // Adjust wind strength based on weather condition
            newWindSpeed += currentWeatherPreset.WindSpeedModifier;
        }

        #endregion

        private void FixedUpdate()
        {
            UpdateWind();
        }

        private void UpdateWind()
        {
            var randomFactor = Random.Range(0.8f, 1.2f);
            windSpeed = Mathf.Lerp(windSpeed, newWindSpeed * randomFactor, windSpeedChangeRate);

            windDirection = Mathf.LerpAngle(windDirection, newWindDirection * randomFactor, windDirectionChangeRate);

            //New wind direction
            if (timeSinceLastWindChange > Time.time)
                return;

            timeSinceLastWindChange = Random.Range(minTimeBetweenWindChanges, maxTimeBetweenWindChanges) + Time.time;
            ChangeWind();
        }

        private void ChangeWind()
        {
            if (currentWeatherPreset == null)
                return;

            newWindSpeed = Random.Range(minWindSpeed, maxWindSpeed) + currentWeatherPreset.WindSpeedModifier;
            windSpeedChangeRate = Random.Range(minWindSpeedChangeRate, maxWindSpeedChangeRate);

            newWindDirection = Random.Range(0f, 360f);
            windDirectionChangeRate = Random.Range(minWindDirectionChangeRate, maxWindDirectionChangeRate);

            Debug.Log("Changing wind to: " + newWindDirection + "degrees at " + newWindSpeed + " knots.");
        }

        private void SetNewWind()
        {
            windSpeed = Random.Range(minWindSpeed, maxWindSpeed);
            newWindSpeed = windSpeed;
            windSpeedChangeRate = WindSpeedGenerator.GenerateWindSpeed(minWindSpeed, maxWindSpeed, meanWindSpeed);

            windDirection = Random.Range(0f, 360f);
            newWindDirection = windDirection;
            windDirectionChangeRate = Random.Range(minWindDirectionChangeRate, maxWindDirectionChangeRate);

            timeSinceLastWindChange = Random.Range(minTimeBetweenWindChanges, maxTimeBetweenWindChanges) + Time.time;

            Debug.Log("Changing wind to: " + newWindDirection + "degrees at " + newWindSpeed + " knots.");
        }
    }
}

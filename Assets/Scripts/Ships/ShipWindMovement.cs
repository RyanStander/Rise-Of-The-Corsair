using System;
using Events;
using Ships.Enums;
using UnityEngine;

namespace Ships
{
    /// <summary>
    /// moves the ship based on the current wind direction and the best sailing point of the ship
    /// </summary>
    public class ShipWindMovement : MonoBehaviour
    {
        [SerializeField] private ShipData shipData;
        [SerializeField] private Rigidbody shipRigidbody;


        #region Private Variables

        private float currentWindDirection;
        private float currentWindSpeed;

        #region Sailing Directions

        private float beforeTheWind = 0f;
        private float runningBroadReach = 22.5f;
        private float broadReach = 45f;
        private float broadBeamReach = 67.5f;
        private float beamReach = 90f;
        private float closeHauledBeamReach = 112.5f;
        private float closeHauled = 135f;
        private float closeHauledIntoTheEye = 157.5f;
        private float intoTheEye = 180f;

        private float bestSailingDirection;

        #endregion

        #endregion

        #region OnEvents

        private void OnValidate()
        {
            if (shipData == null)
                shipData = GetComponent<ShipData>();

            if (shipRigidbody == null)
                shipRigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventIdentifiers.NewWind, OnNewWind);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventIdentifiers.NewWind, OnNewWind);
        }

        private void OnNewWind(EventData eventData)
        {
            if (!eventData.IsEventOfType(out NewWind newWind))
                return;

            currentWindDirection = newWind.WindDirection;
            currentWindSpeed = newWind.WindSpeed;
        }

        #endregion

        private void Awake()
        {
            DetermineBestSailingPoint();
        }

        private void FixedUpdate()
        {
            MoveShip();
        }

        private void DetermineBestSailingPoint()
        {
            switch (shipData.Stats.bestSailingPoint)
            {
                case ShipWindDirections.BeforeTheWind:
                    bestSailingDirection = beforeTheWind;
                    break;
                case ShipWindDirections.RunningBroadReach:
                    bestSailingDirection = runningBroadReach;
                    break;
                case ShipWindDirections.BroadReach:
                    bestSailingDirection = broadReach;
                    break;
                case ShipWindDirections.BroadBeamReach:
                    bestSailingDirection = broadBeamReach;
                    break;
                case ShipWindDirections.BeamReach:
                    bestSailingDirection = beamReach;
                    break;
                case ShipWindDirections.CloseHauledBeamReach:
                    bestSailingDirection = closeHauledBeamReach;
                    break;
                case ShipWindDirections.CloseHauled:
                    bestSailingDirection = closeHauled;
                    break;
                case ShipWindDirections.CloseHauledIntoTheEye:
                    bestSailingDirection = closeHauledIntoTheEye;
                    break;
                case ShipWindDirections.IntoTheEye:
                    bestSailingDirection = intoTheEye;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Determines the forward force to apply to the ship based on the best sailing point and the current wind speed and direction with the ship's speed modifier
        /// </summary>
        private void MoveShip()
        {
            //the ships highest speed is when the angle matches the best sailing point, the higher it becomes the less speed the ship has, anything below the best sailing point stays the same
            var speedModifier = DetermineSpeedModifier();

            //TODO: replace 50 with currentWindSpeed
            var force = transform.forward * Mathf.Clamp(50 * speedModifier * shipData.Stats.speedModifier,
                shipData.Stats.minSpeed, shipData.Stats.maxSpeed);
            //Debug.Log($"Wind Speed: {currentWindSpeed} | Speed Modifier: {speedModifier} | Ship Speed Modifier: {shipData.Stats.speedModifier}");

            //debug of all the values and final force result
            //apply the force to the ship
            shipRigidbody.AddForce(force);
        }

        private float DetermineSpeedModifier()
        {
            //angle between the current wind direction and the ship's rotation
            var currentDeltaAngle = Mathf.Abs(Mathf.DeltaAngle(currentWindDirection, transform.eulerAngles.y));

            //the speed modifer is highest when the angle matches the best sailing point,
            //the higher it becomes the less speed the ship has, anything below the best sailing point stays the same
            var speedModifier =
                Mathf.Clamp(1 - (Mathf.Max(currentDeltaAngle - bestSailingDirection,0) / Mathf.Max(180 - bestSailingDirection,0.001f)), 0, 1);

            return speedModifier;
        }
    }
}
//1-(0/180),0,1)=1
//1-(45/180),0,1)=0.75
//1-(180/180)=0

using System;
using Ships;
using Ships.Enums;
using UnityEngine;

namespace AI.Ships
{
    public class AIShipSteering : ShipSteering
    {
        private Rigidbody shipRigidbody;
        [SerializeField] private float distanceToPlayer = 20;
        [SerializeField] private GameObject playerShip;

        private float turnModifier = 1f;
        private float maneuverabilityModifier = 1f;

        protected override void GetReferences()
        {
            base.GetReferences();

            if (shipRigidbody == null)
                shipRigidbody = GetComponent<Rigidbody>();

            if (playerShip == null)
                playerShip = GameObject.FindGameObjectWithTag("Player");
        }

        private void Awake()
        {
            if (playerShip == null)
                playerShip = GameObject.FindGameObjectWithTag("Player");

            maneuverabilityModifier = GetManeuverabilityModifier();
        }

        private void Update()
        {
            TurnShip();
        }

        //If the ship is a certain distance away from the player, it will go directly towards the player, when within the range it will try to circle the player
        private void TurnShip()
        {
            var turnMod = Vector3.up * Mathf.Clamp(turnModifier * 1.25f * Time.deltaTime * maneuverabilityModifier *
                                                   shipRigidbody.velocity.magnitude, 0.5f, 3f);

            //determine if the ship is within the range of the player
            if (Vector3.Distance(transform.position, playerShip.transform.position) > distanceToPlayer)
            {
                var direction = playerShip.transform.position - transform.position;
                var toRotation = Quaternion.LookRotation(direction, transform.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnModifier * Time.deltaTime);
            }
            else
            {
                //determine which side of the ship the player is on
                var aimDirection = DetermineAimDirection();

                var direction = playerShip.transform.position - transform.position;

                var toRotation = Quaternion.LookRotation(direction, transform.up);

                switch (aimDirection)
                {
                    case ShipSide.Starboard://Right direction
                        toRotation*= Quaternion.Euler(0, 90, 0);
                        break;
                    case ShipSide.Port://Left direction
                        toRotation*= Quaternion.Euler(0, -90, 0);
                        break;
                    case ShipSide.Bow:
                        break;
                    case ShipSide.Stern:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnModifier * Time.deltaTime);
            }
        }

        private ShipSide DetermineAimDirection()
        {
            //based on the position of the main camera and the ships position, determine whether the camera is to the left or right of the ship
            var playerPosition = playerShip.transform.position;
            var shipPosition = transform.position;
            var playerDirection = playerPosition - shipPosition;
            var shipDirection = transform.forward;
            var crossProduct = Vector3.Cross(playerDirection, shipDirection);

            return crossProduct.y switch
            {
                > 0 => ShipSide.Starboard,
                < 0 => ShipSide.Port,
                _ => ShipSide.Starboard
            };
        }

        private float GetManeuverabilityModifier()
        {
            return shipData.Stats.maneuverability switch
            {
                ShipManeuverability.Low => 1,
                ShipManeuverability.LowMid => 1.25f,
                ShipManeuverability.Mid => 1.5f,
                ShipManeuverability.MidHigh => 1.75f,
                ShipManeuverability.High => 2f,
                ShipManeuverability.VeryHigh => 2.25f,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}

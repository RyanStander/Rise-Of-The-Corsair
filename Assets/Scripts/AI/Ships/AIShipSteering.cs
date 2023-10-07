using System;
using Ships;
using Ships.Enums;
using UnityEngine;

namespace AI.Ships
{
    public class AIShipSteering : ShipSteering
    {
        [SerializeField] private float distanceToPlayer = 20;
        [SerializeField] private GameObject playerShip;

        protected override void GetReferences()
        {
            base.GetReferences();

            if (playerShip == null)
                playerShip = GameObject.FindGameObjectWithTag("Player");
        }

        protected override void Awake()
        {
            base.Awake();

            if (playerShip == null)
                playerShip = GameObject.FindGameObjectWithTag("Player");
        }

        //If the ship is a certain distance away from the player, it will go directly towards the player, when within the range it will try to circle the player
        protected override void TurnShip()
        {
            var turnMod = Mathf.Clamp(turnModifier * 1.25f * Time.deltaTime * maneuverabilityModifier *
                                      shipRigidbody.velocity.magnitude, 0.5f, 3f);

            //determine if the ship is within the range of the player
            if (Vector3.Distance(transform.position, playerShip.transform.position) > distanceToPlayer)
            {
                var direction = playerShip.transform.position - transform.position;
                var toRotation = Quaternion.LookRotation(direction, transform.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnMod * Time.deltaTime);

                shipSway.UpdateSway(false, false);
            }
            else
            {
                //determine which side of the ship the player is on
                var aimDirection = DetermineAimDirection();

                var direction = playerShip.transform.position - transform.position;

                var toRotation = Quaternion.LookRotation(direction, transform.up);

                switch (aimDirection)
                {
                    case ShipSide.Starboard: //Right direction
                        toRotation *= Quaternion.Euler(0, 90, 0);
                        shipSway.UpdateSway(false, true);
                        break;
                    case ShipSide.Port: //Left direction
                        toRotation *= Quaternion.Euler(0, -90, 0);
                        shipSway.UpdateSway(true, true);
                        break;
                    case ShipSide.Bow:
                        shipSway.UpdateSway(false, false);
                        break;
                    case ShipSide.Stern:
                        shipSway.UpdateSway(false, false);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnMod * Time.deltaTime);
            }

            //set the x and z rotation back to 0
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }

        public bool IsChasing()
        {
            return Vector3.Distance(transform.position, playerShip.transform.position) > distanceToPlayer;
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
    }
}

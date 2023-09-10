using System;
using Ships;
using Ships.Enums;
using UnityEngine;

namespace AI.Ships
{
    public class AIShipFiring : MonoBehaviour
    {
        [SerializeField] private ShipData shipData;
        [SerializeField] private CannonPointHolder cannonPointHolder;
        [SerializeField] private ShipReloading shipReloading;
        [SerializeField] private AIShipSteering aiShipSteering;
        [SerializeField] private GameObject playerShip;

        private void OnValidate()
        {
            if (shipData == null)
                shipData = GetComponent<ShipData>();
            if (cannonPointHolder == null)
                cannonPointHolder = GetComponentsInChildren<CannonPointHolder>(true)[0];
            if (shipReloading == null)
                shipReloading = GetComponent<ShipReloading>();
            if (aiShipSteering == null)
                aiShipSteering = GetComponent<AIShipSteering>();
            if (playerShip == null)
                playerShip = GameObject.FindGameObjectWithTag("Player");
        }

        private void Start()
        {
            if (playerShip == null)
                playerShip = GameObject.FindGameObjectWithTag("Player");
        }

        public void HandleShipFiring()
        {
            AttemptFire();
        }

        private void AttemptFire()
        {
            //for now, only fire if the ship is circling the player TODO: change when implementing other directions
            if (aiShipSteering.isChasing())
                return;

            //check the direction of the player to determine which side to fire on
            var aimDirection = DetermineAimDirection();

            //check if the ship can fire on that side
            if (shipReloading.CanFire(aimDirection))
            {
                //fire the cannons
                cannonPointHolder.FireCannons(8, aimDirection);
                shipReloading.StartReload(aimDirection);
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
                < 0 => ShipSide.Starboard,
                > 0 => ShipSide.Port,
                _ => ShipSide.Starboard
            };
        }
    }
}

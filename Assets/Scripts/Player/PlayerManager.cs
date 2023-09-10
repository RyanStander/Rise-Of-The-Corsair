using System;
using Ships;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(ShipData))]
    [RequireComponent(typeof(ShipWindMovement))]
    [RequireComponent(typeof(PlayerShipSteering))]
    [RequireComponent(typeof(PlayerFiring))]
    [RequireComponent(typeof(PlayerAiming))]
    [RequireComponent(typeof(ShipReloading))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private ShipData shipData;
        [SerializeField] private ShipWindMovement shipWindMovement;
        [SerializeField] private PlayerShipSteering playerShipSteering;
        [SerializeField] private PlayerFiring playerFiring;
        [SerializeField] private PlayerAiming playerAiming;
        [SerializeField] private ShipReloading shipReloading;

        private void OnValidate()
        {
            SetupPlayer();
        }

        private void SetupPlayer()
        {
            if (shipData == null)
                shipData = GetComponent<ShipData>();
            if (shipWindMovement == null)
                shipWindMovement = GetComponent<ShipWindMovement>();
            if (playerShipSteering == null)
                playerShipSteering = GetComponent<PlayerShipSteering>();
            if (playerFiring == null)
                playerFiring = GetComponent<PlayerFiring>();
            if (playerAiming == null)
                playerAiming = GetComponent<PlayerAiming>();
            if (shipReloading == null)
                shipReloading = GetComponent<ShipReloading>();
        }

        private void FixedUpdate()
        {
            shipWindMovement.HandleShipMovement();
            playerShipSteering.HandleShipSteering();

            playerAiming.HandlePlayerAiming();
            playerFiring.HandlePlayerFiring();
        }
    }
}

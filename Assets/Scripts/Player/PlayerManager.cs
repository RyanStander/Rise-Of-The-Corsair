using System;
using Ships;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerShipSteering))]
    [RequireComponent(typeof(PlayerFiring))]
    [RequireComponent(typeof(PlayerAiming))]

    public class PlayerManager : ShipManager
    {
        [SerializeField] private PlayerShipSteering playerShipSteering;
        [SerializeField] private PlayerFiring playerFiring;
        [SerializeField] private PlayerAiming playerAiming;

        protected override void OnValidate()
        {
            base.OnValidate();
            SetupPlayer();
        }

        private void SetupPlayer()
        {
            if (playerShipSteering == null)
                playerShipSteering = GetComponent<PlayerShipSteering>();
            if (playerFiring == null)
                playerFiring = GetComponent<PlayerFiring>();
            if (playerAiming == null)
                playerAiming = GetComponent<PlayerAiming>();
        }

        private void FixedUpdate()
        {
            playerAiming.HandlePlayerAiming();

            if (ShipData.IsSunk)
            {
                Animator.SetBool(IsSunk, true);
                return;
            }

            ShipWindMovement.HandleShipMovement();
            playerShipSteering.HandleShipSteering();

            playerFiring.HandlePlayerFiring();
        }
    }
}

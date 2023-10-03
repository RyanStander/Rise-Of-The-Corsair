using System;
using Events;
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

        #region On Events

        protected override void OnValidate()
        {
            base.OnValidate();
            SetupPlayer();
        }

        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventIdentifiers.RecalculatePlayerCrewModifiers, OnSortPlayers);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventIdentifiers.RecalculatePlayerCrewModifiers, OnSortPlayers);
        }

        private void OnSortPlayers(EventData eventData)
        {
            if (!eventData.IsEventOfType(out RecalculatePlayerCrewModifiers _))
                return;

            ShipModifiers.CalculateModifiers(ShipData);
        }

        #endregion



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
                Animator.SetBool(isSunk, true);
                return;
            }

            ShipWindMovement.HandleShipMovement();
            playerShipSteering.HandleShipSteering();

            playerFiring.HandlePlayerFiring();
        }
    }
}

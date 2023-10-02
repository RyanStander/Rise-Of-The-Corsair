using System;
using Ships;
using UnityEngine;

namespace AI.Ships
{
    [RequireComponent(typeof(AIShipSteering))]
    [RequireComponent(typeof(AIShipFiring))]
    public class AIShipManager : ShipManager
    {
        [SerializeField] private AIShipSteering aiShipSteering;
        [SerializeField] private AIShipFiring aiShipFiring;

        protected override void OnValidate()
        {
            base.OnValidate();

            SetupAI();
        }

        private void SetupAI()
        {
            if(aiShipSteering == null)
                aiShipSteering = GetComponent<AIShipSteering>();
            if(aiShipFiring == null)
                aiShipFiring = GetComponent<AIShipFiring>();
        }

        private void FixedUpdate()
        {
            if (ShipData.IsSunk)
            {
                Animator.SetBool(IsSunk, true);
                return;
            }

            ShipWindMovement.HandleShipMovement();
            aiShipSteering.HandleShipSteering();

            aiShipFiring.HandleShipFiring();
        }
    }
}

using System;
using Ships;
using UnityEngine;

namespace AI.Ships
{
    [RequireComponent(typeof(ShipData))]
    [RequireComponent(typeof(ShipWindMovement))]
    [RequireComponent(typeof(ShipHealth))]
    [RequireComponent(typeof(AIShipSteering))]
    [RequireComponent(typeof(AIShipFiring))]
    [RequireComponent(typeof(ShipReloading))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class AIShipManager : MonoBehaviour
    {
        [SerializeField] private ShipData shipData;
        [SerializeField] private ShipWindMovement shipWindMovement;
        [SerializeField] private ShipReloading shipReloading;
        [SerializeField] private AIShipSteering aiShipSteering;
        [SerializeField] private AIShipFiring aiShipFiring;
        [SerializeField] private Animator animator;
        private static readonly int IsSunk = Animator.StringToHash("IsSunk");

        private void OnValidate()
        {
            SetupAI();
        }

        private void SetupAI()
        {
            if(shipData == null)
                shipData = GetComponent<ShipData>();
            if(shipWindMovement == null)
                shipWindMovement = GetComponent<ShipWindMovement>();
            if(shipReloading == null)
                shipReloading = GetComponent<ShipReloading>();
            if(aiShipSteering == null)
                aiShipSteering = GetComponent<AIShipSteering>();
            if(aiShipFiring == null)
                aiShipFiring = GetComponent<AIShipFiring>();
            if (animator == null)
                animator = GetComponentInChildren<Animator>();
        }

        private void FixedUpdate()
        {
            if (shipData.IsSunk)
            {
                animator.SetBool(IsSunk, true);
                return;
            }

            shipWindMovement.HandleShipMovement();
            aiShipSteering.HandleShipSteering();

            aiShipFiring.HandleShipFiring();
        }
    }
}

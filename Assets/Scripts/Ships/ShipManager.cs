using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ships
{
    [RequireComponent(typeof(ShipData))]
    [RequireComponent(typeof(ShipWindMovement))]
    [RequireComponent(typeof(ShipHealth))]
    [RequireComponent(typeof(ShipReloading))]
    [RequireComponent(typeof(ShipModifiers))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class ShipManager : MonoBehaviour
    {
        [field: SerializeField] public ShipData ShipData{ get;private set; }
        [SerializeField] protected ShipWindMovement ShipWindMovement;
        [SerializeField] protected ShipReloading ShipReloading;
        [field: SerializeField] public ShipModifiers ShipModifiers { get;private set; }
        [FormerlySerializedAs("animator")] [SerializeField] protected Animator Animator;
        protected static readonly int isSunk = Animator.StringToHash("IsSunk");

        protected virtual void OnValidate()
        {
            SetupShip();
        }

        private void Start()
        {
            ShipModifiers.CalculateModifiers(ShipData);
        }

        private void SetupShip()
        {
            if (ShipData == null)
                ShipData = GetComponent<ShipData>();
            if(ShipWindMovement == null)
                ShipWindMovement = GetComponent<ShipWindMovement>();
            if(ShipReloading == null)
                ShipReloading = GetComponent<ShipReloading>();
            if (ShipModifiers == null)
                ShipModifiers = GetComponent<ShipModifiers>();
            if (Animator == null)
                Animator = GetComponentInChildren<Animator>();
        }
    }
}

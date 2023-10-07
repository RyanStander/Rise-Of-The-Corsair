using System;
using Ships.Enums;
using UnityEngine;

namespace Ships
{
    public class ShipSteering : MonoBehaviour
    {
        [SerializeField] protected ShipData shipData;
        [SerializeField] protected Rigidbody shipRigidbody;
        [SerializeField] protected GameObject childShipGameObject;

        protected ShipSway shipSway;
        protected float turnModifier = 1f;
        protected float maneuverabilityModifier = 1f;

        private void OnValidate()
        {
            GetReferences();
        }

        protected virtual void GetReferences()
        {
            if (shipData == null)
                shipData = GetComponent<ShipData>();

            if (shipRigidbody == null)
                shipRigidbody = GetComponent<Rigidbody>();
        }

        protected virtual void Awake()
        {
            shipSway = new ShipSway(childShipGameObject);

            maneuverabilityModifier = GetManeuverabilityModifier();
        }

        private float GetManeuverabilityModifier()
        {
            return shipData.Stats.Maneuverability switch
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

        public void HandleShipSteering()
        {
            TurnShip();
        }

        protected virtual void TurnShip()
        {
            throw new NotImplementedException();
        }
    }
}

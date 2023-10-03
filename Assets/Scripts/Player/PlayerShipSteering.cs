using System;
using Ships;
using Ships.Enums;
using UnityEngine;

namespace Player
{
    public class PlayerShipSteering : ShipSteering
    {
        [SerializeField] private Rigidbody shipRigidbody;

        private float turnModifier = 1f;
        private float maneuverabilityModifier = 1f;

        protected override void GetReferences()
        {
            base.GetReferences();

            if (shipRigidbody == null)
                shipRigidbody = GetComponent<Rigidbody>();
        }

        private void Awake()
        {
            maneuverabilityModifier = GetManeuverabilityModifier();
        }

        public void HandleShipSteering()
        {
            TurnShip();
        }

        //When A or D is held down, turn the ship based on its maneuverability
        private void TurnShip()
        {
            var turnMod = Vector3.up * Mathf.Clamp(turnModifier * 1.25f * Time.deltaTime * maneuverabilityModifier *
                                                   shipRigidbody.velocity.magnitude, 0.5f, 3f);

            //Check if the ship is turning left or right
            if (Input.GetKey(KeyCode.A))
            {
                shipRigidbody.AddTorque(-turnMod*100);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                shipRigidbody.AddTorque(turnMod*100);
            }
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
    }
}

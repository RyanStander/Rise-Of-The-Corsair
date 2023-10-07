using System;
using Ships;
using Ships.Enums;
using UnityEngine;

namespace Player
{
    public class PlayerShipSteering : ShipSteering
    {
        //When A or D is held down, turn the ship based on its maneuverability
        protected override void TurnShip()
        {
            var turnStrength = 1.25f * Time.deltaTime * maneuverabilityModifier * shipRigidbody.velocity.magnitude;

            var turnMod = Vector3.up * Mathf.Clamp(turnModifier * turnStrength, 0.5f, 3f);

            //Check if the ship is turning left or right
            if (Input.GetKey(KeyCode.A))
            {
                shipRigidbody.AddTorque(-turnMod * 100);
                shipSway.UpdateSway(true, true);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                shipRigidbody.AddTorque(turnMod * 100);
                shipSway.UpdateSway(false, true);
            }
            else
            {
                shipSway.UpdateSway(false, false);
            }
        }
    }
}

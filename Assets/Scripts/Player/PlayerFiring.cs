using System;
using Ships;
using Ships.Enums;
using UnityEngine;

namespace Player
{
    public class PlayerFiring : MonoBehaviour
    {
        [SerializeField] private ShipData shipData;
        [SerializeField] private CannonPointHolder cannonPointHolder;
        [SerializeField] private PlayerAiming playerAiming;

        private void OnValidate()
        {
            if (shipData == null)
                shipData = GetComponent<ShipData>();
            if (cannonPointHolder == null)
                cannonPointHolder = GetComponentsInChildren<CannonPointHolder>(true)[0];
            if (playerAiming == null)
                playerAiming = GetComponent<PlayerAiming>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //cannonPointHolder.FireCannons(shipData.SideCannonCount, ShipSide.Starboard);
                cannonPointHolder.FireCannons(6, playerAiming.CurrentAimSide);
            }
        }
    }
}

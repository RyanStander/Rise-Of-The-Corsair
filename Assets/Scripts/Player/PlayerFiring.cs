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

        private void OnValidate()
        {
            if (shipData == null)
            {
                shipData = GetComponent<ShipData>();
            }

            if (cannonPointHolder == null)
            {
                cannonPointHolder = GetComponentsInChildren<CannonPointHolder>(true)[0];
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //cannonPointHolder.FireCannons(shipData.SideCannonCount, ShipSide.Starboard);
                cannonPointHolder.FireCannons(6, ShipSide.Port);
            }
        }
    }
}

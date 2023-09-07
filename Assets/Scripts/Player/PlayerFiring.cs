using Ships;
using Ships.Enums;
using UnityEngine;

namespace Player
{
    public class PlayerFiring : MonoBehaviour
    {
        [SerializeField] private ShipData shipData;
        [SerializeField] private CannonPointHolder cannonPointHolder;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                cannonPointHolder.FireCannons(shipData.SideCannonCount, ShipSide.Starboard);
            }
        }
    }
}

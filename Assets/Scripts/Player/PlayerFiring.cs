using Ships;
using UnityEngine;

namespace Player
{
    public class PlayerFiring : MonoBehaviour
    {
        [SerializeField] private ShipData shipData;
        [SerializeField] private CannonPointHolder cannonPointHolder;
        [SerializeField] private PlayerAiming playerAiming;
        [SerializeField] private ShipReloading shipReloading;

        private void OnValidate()
        {
            if (shipData == null)
                shipData = GetComponent<ShipData>();
            if (cannonPointHolder == null)
                cannonPointHolder = GetComponentsInChildren<CannonPointHolder>(true)[0];
            if (playerAiming == null)
                playerAiming = GetComponent<PlayerAiming>();
            if (shipReloading == null)
                shipReloading = GetComponent<ShipReloading>();
        }

        private void Update()
        {
            if (shipReloading.CanFire() && Input.GetKeyDown(KeyCode.Space))
            {
                //cannonPointHolder.FireCannons(shipData.SideCannonCount, ShipSide.Starboard);
                cannonPointHolder.FireCannons(6, playerAiming.CurrentAimSide);
                shipReloading.StartReload();
            }
        }
    }
}

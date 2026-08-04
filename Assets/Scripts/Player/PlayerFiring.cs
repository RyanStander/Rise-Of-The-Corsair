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

        public void HandlePlayerFiring()
        {
            if (shipReloading.CanFire(playerAiming.CurrentAimSide) && Input.GetKeyDown(KeyCode.Mouse0))
            {
                cannonPointHolder.FireCannons(playerAiming.CurrentAimSide);
                shipReloading.StartReload(playerAiming.CurrentAimSide);
            }
        }
    }
}

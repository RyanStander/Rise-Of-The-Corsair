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
                var a = DetermineFireAngle();
                Debug.Log(a);

                cannonPointHolder.FireCannons(6, playerAiming.CurrentAimSide,a);
                shipReloading.StartReload(playerAiming.CurrentAimSide);
            }
        }

        /// <summary>
        /// raycast from the camera to the mouse position and determine the angle to fire at
        /// </summary>
        private Vector3  DetermineFireAngle()
        {
            // Raycast from the mouse position into the game world
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Calculate the direction from the cannon muzzle to the hit point
                Vector3 direction = hit.point - transform.position;

                return direction.normalized;

                // Create the cannonball
                /*GameObject cannonball = Instantiate(cannonballPrefab, cannonMuzzle.position, Quaternion.identity);
                Rigidbody rb = cannonball.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    // Calculate the force vector
                    Vector3 force = direction.normalized * cannonballSpeed;

                    // Apply the force to the cannonball
                    rb.AddForce(force, ForceMode.VelocityChange);
                }*/
            }

            return Vector3.zero;
        }
    }
}

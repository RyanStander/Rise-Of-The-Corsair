using System;
using System.Collections;
using Projectiles;
using Ships.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ships
{
    /// <summary>
    /// Holds the points of the each cannon on the ship. This holds the maximum amount of points for the ship.
    /// </summary>
    public class CannonPointHolder : MonoBehaviour
    {
        [SerializeField] private GameObject cannonBallPrefab;
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private AudioClip cannonFireSound;
        [SerializeField] private float cannonBallSpeed = 120;
        [SerializeField] private Vector2 minToMaxFireDelay = new Vector2(0, 0.5f);
        [field: SerializeField] private CannonPoint[] cannonPoints;

        private void OnValidate()
        {
            cannonPoints = GetComponentsInChildren<CannonPoint>(true);
        }

        #region Straight Fire

        public void FireCannons(ShipSide firingSide)
        {
            foreach (CannonPoint cannonPoint in cannonPoints)
            {
                if (cannonPoint.CannonZone == firingSide && cannonPoint.IsCannonInstalled)
                {
                    StartCoroutine(FireCannon(Random.Range(minToMaxFireDelay.x, minToMaxFireDelay.y),
                        cannonPoint.CannonPointTransform, cannonPoint.CannonAudioSource));
                }
            }
        }

        private IEnumerator FireCannon(float waitTime, Transform cannonPoint, AudioSource cannonAudioSource)
        {
            yield return new WaitForSeconds(waitTime);

            var position = cannonPoint.position;
            var rotation = cannonPoint.rotation;

            //Instantiate cannonball
            var cannonBall = Instantiate(cannonBallPrefab, position, rotation);
            //get rigidbody and add force in the forward direction
            var projectile = cannonBall.GetComponent<BaseProjectile>();
            projectile.SetProjectile(cannonPoint.forward * cannonBallSpeed, transform.root);

            //Play cannon fire sound
            cannonAudioSource.PlayOneShot(cannonFireSound);

            //Instantiate explosion
            Instantiate(explosionPrefab, position, rotation);
        }

        #endregion

        public void InstallCannon(int cannonID, GameObject cannonPrefab)
        {
            cannonPoints[cannonID].InstallCannon(cannonPrefab);
        }

        public int GetTotalCannonCount()
        {
            return cannonPoints.Length;
        }
    }
}

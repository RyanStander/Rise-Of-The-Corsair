using System;
using System.Collections;
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
        [SerializeField] private float cannonBallSpeed;
        [SerializeField] private Vector2 minToMaxFireDelay = new Vector2(0, 0.5f);
        [field: SerializeField] public Transform[] StarboardCannonPoints { get; private set; }
        [field: SerializeField] public Transform[] PortCannonPoints { get; private set; }

        public void FireCannons(int sideCannonCount, ShipSide firingSide)
        {
            switch (firingSide)
            {
                case ShipSide.Starboard:
                    FireSpecifiedCannon(StarboardCannonPoints, sideCannonCount);
                    break;
                case ShipSide.Port:
                    FireSpecifiedCannon(PortCannonPoints, sideCannonCount);
                    break;
                case ShipSide.Bow:
                    break;
                case ShipSide.Stern:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(firingSide), firingSide, null);
            }
        }

        private void FireSpecifiedCannon(Transform[] cannonPoints, int sideCannonCount)
        {
            for (var i = 0; i < sideCannonCount; i++)
            {
                if (i >= cannonPoints.Length)
                {
                    Debug.LogError("You are trying to fire more cannons than there are on the ship!");
                    return;
                }

                StartCoroutine(FireCannon(Random.Range(minToMaxFireDelay.x, minToMaxFireDelay.y), cannonPoints[i]));
            }
        }

        private IEnumerator FireCannon(float waitTime, Transform cannonPoint)
        {
            yield return new WaitForSeconds(waitTime);

            var position = cannonPoint.position;
            var rotation = cannonPoint.rotation;

            //Instantiate cannonball
            var cannonBall = Instantiate(cannonBallPrefab, position, rotation);
            //get rigidbody and add force in the forward direction
            var cannonBallRb = cannonBall.GetComponent<Rigidbody>();
            cannonBallRb.AddForce(cannonPoint.forward * cannonBallSpeed, ForceMode.Impulse);

            //Instantiate explosion
            Instantiate(explosionPrefab, position, rotation);
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Projectiles
{
    public class BaseProjectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody projectileRigidbody;

        private const float DeSpawnHeight = -20f;
        private const float WaitTime = 2;

        private bool isDeSpawning;
        private float timeStamp;

        private void OnValidate()
        {
            if (projectileRigidbody == null)
            {
                projectileRigidbody = GetComponent<Rigidbody>();
            }
        }

        private void FixedUpdate()
        {
            HandleDeSpawn();
        }

        private void HandleDeSpawn()
        {
            if (!isDeSpawning && transform.position.y < DeSpawnHeight)
            {
                //Stop the rigidbody from moving
                projectileRigidbody.velocity = Vector3.zero;
                projectileRigidbody.angularVelocity = Vector3.zero;
                projectileRigidbody.useGravity = false;
                isDeSpawning = true;
                timeStamp = Time.time + WaitTime;
            }

            if (isDeSpawning & timeStamp <= Time.time)
            {
                Destroy(gameObject);
            }
        }
    }
}

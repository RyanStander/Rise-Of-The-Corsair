using System;
using Ships;
using Ships.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Projectiles
{
    public class BaseProjectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody projectileRigidbody;
        [SerializeField] private MeshRenderer projectileMeshRenderer;
        [SerializeField] private float projectileDamage = 10;
        [SerializeField] private ProjectileType projectileType = ProjectileType.RoundShot;

        private const float DeSpawnHeight = -20f;
        private const float WaitTime = 2;

        private bool isDeSpawning;
        private float timeStamp;

        private void OnValidate()
        {
            if (projectileRigidbody == null)
                projectileRigidbody = GetComponent<Rigidbody>();

            if (projectileMeshRenderer == null)
                projectileMeshRenderer = GetComponent<MeshRenderer>();
        }

        private void FixedUpdate()
        {
            HandleDeSpawn();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root == transform.root)
                return;

            ShipPart partHit;

            if (other.CompareTag(ShipPart.Hull.ToString()))
            {
                partHit = ShipPart.Hull;
            }
            else if (other.CompareTag(ShipPart.Sail.ToString()))
            {
                partHit = ShipPart.Sail;
            }
            else if (other.CompareTag(ShipPart.Mast.ToString()))
            {
                partHit = ShipPart.Mast;
            }
            else if (other.CompareTag(ShipPart.Cannon.ToString()))
            {
                partHit = ShipPart.Cannon;
            }
            else if (other.CompareTag(ShipPart.Crew.ToString()))
            {
                partHit = ShipPart.Crew;
            }
            else
            {
                return;
            }

            if (other.transform.root.TryGetComponent<ShipHealth>(out var shipHealth))
            {
                shipHealth.TakeDamage(projectileDamage, partHit, projectileType);
                InitiateDeSpawn();
            }
        }

        private void HandleDeSpawn()
        {
            if (!isDeSpawning && transform.position.y < DeSpawnHeight)
            {
                InitiateDeSpawn();
            }

            if (isDeSpawning & timeStamp <= Time.time)
            {
                Destroy(gameObject);
            }
        }

        private void InitiateDeSpawn()
        {
            //Stop the rigidbody from moving
            projectileRigidbody.velocity = Vector3.zero;
            projectileRigidbody.angularVelocity = Vector3.zero;
            projectileRigidbody.useGravity = false;
            projectileMeshRenderer.enabled = false;
            isDeSpawning = true;
            timeStamp = Time.time + WaitTime;
        }
    }
}

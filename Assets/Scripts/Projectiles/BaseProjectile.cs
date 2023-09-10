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

        [SerializeField] private ProjectileEffects hitEffects;


        private Transform originShipTransform;
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

        public void SetProjectile(Vector3 force, Transform originShipTransform)
        {
            this.originShipTransform = originShipTransform;
            projectileRigidbody.AddForce(force, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            //avoid hitting the ship that fired the projectile
            if (other.transform.root == originShipTransform)
                return;

            ShipPart partHit;

            if (other.CompareTag(ShipPart.Hull.ToString()))
            {
                partHit = ShipPart.Hull;
                CreateHitEffect(hitEffects.hitEffectOnHull, other.transform);
            }
            else if (other.CompareTag(ShipPart.Sail.ToString()))
            {
                partHit = ShipPart.Sail;
                CreateHitEffect(hitEffects.hitEffectOnSail, other.transform);
            }
            else if (other.CompareTag(ShipPart.Mast.ToString()))
            {
                partHit = ShipPart.Mast;
                CreateHitEffect(hitEffects.hitEffectOnMast, other.transform);
            }
            else if (other.CompareTag(ShipPart.Cannon.ToString()))
            {
                partHit = ShipPart.Cannon;
                CreateHitEffect(hitEffects.hitEffectOnCannon, other.transform);
            }
            else if (other.CompareTag(ShipPart.Crew.ToString()))
            {
                partHit = ShipPart.Crew;
                CreateHitEffect(hitEffects.hitEffectOnCrew, other.transform);
            }
            else
            {
                return;
            }

            if (other.transform.root.TryGetComponent<ShipHealth>(out var shipHealth))
            {
                shipHealth.TakeDamage(projectileDamage, partHit, projectileType);
                if (partHit != ShipPart.Sail)
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

        private void CreateHitEffect(GameObject hitEffect, Transform hitTransform)
        {
            var effect = Instantiate(hitEffect, hitTransform);
            effect.transform.position = transform.position;
        }
    }
}

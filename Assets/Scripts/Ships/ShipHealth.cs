using System;
using System.Collections.Generic;
using Projectiles;
using Ships.Enums;
using UnityEngine;

namespace Ships
{
    public class ShipHealth : MonoBehaviour
    {
        [SerializeField] private ShipData shipData;
        public float SailCurrentHealth { get; private set; }
        public float HullCurrentHealth { get; private set; }
        public float MastCurrentHealth { get; private set; }
        public float CrewCurrentHealth { get; private set; }
        public float CannonCurrentHealth { get; private set; }

        private const float nonDirectDamageMultiplier = 0.5f;

        private void OnValidate()
        {
            if (shipData == null)
                shipData = GetComponent<ShipData>();
        }

        private void Awake()
        {
            //Set all current health to max health
            SailCurrentHealth = shipData.Stats.SailMaxHealth;
            HullCurrentHealth = shipData.Stats.HullMaxHealth;
            MastCurrentHealth = shipData.Stats.MastMaxHealth;
            CrewCurrentHealth = 500;//assume 5 health per crew member
            CannonCurrentHealth = 6*50;//assume 50 health per cannon
        }

        public void TakeDamage(float damage, ShipPart partHit, ProjectileType projectileType)
        {
            var damageMultiplier = ShipDamageMultipliers.GetDamageMultiplier(projectileType);

            switch (partHit)
            {
                case ShipPart.Hull:
                    HullCurrentHealth -= CalculateDamage(damage, damageMultiplier[ShipPart.Hull]);
                    CrewCurrentHealth -= CalculateDamage(damage, damageMultiplier[ShipPart.Crew])/nonDirectDamageMultiplier;
                    CannonCurrentHealth -= CalculateDamage(damage, damageMultiplier[ShipPart.Cannon])/nonDirectDamageMultiplier;
                    break;
                case ShipPart.Sail:
                    SailCurrentHealth -= CalculateDamage(damage, damageMultiplier[ShipPart.Sail]);
                    MastCurrentHealth -= CalculateDamage(damage, damageMultiplier[ShipPart.Mast])/nonDirectDamageMultiplier;
                    break;
                case ShipPart.Mast:
                    MastCurrentHealth -= CalculateDamage(damage, damageMultiplier[ShipPart.Mast]);
                    SailCurrentHealth -= CalculateDamage(damage, damageMultiplier[ShipPart.Sail])/nonDirectDamageMultiplier;
                    HullCurrentHealth -= CalculateDamage(damage, damageMultiplier[ShipPart.Hull])/nonDirectDamageMultiplier;
                    break;
                case ShipPart.Cannon:
                    CannonCurrentHealth -= CalculateDamage(damage, damageMultiplier[ShipPart.Cannon]);
                    HullCurrentHealth -= CalculateDamage(damage, damageMultiplier[ShipPart.Hull])/nonDirectDamageMultiplier;
                    CrewCurrentHealth -= CalculateDamage(damage, damageMultiplier[ShipPart.Crew])/nonDirectDamageMultiplier;
                    break;
                case ShipPart.Crew:
                    CrewCurrentHealth -= CalculateDamage(damage, damageMultiplier[ShipPart.Crew]);
                    HullCurrentHealth -= CalculateDamage(damage, damageMultiplier[ShipPart.Hull])/nonDirectDamageMultiplier;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(partHit), partHit, null);
            }

            if (HullCurrentHealth <= 0)
            {
                //ship is destroyed
                shipData.ShipSunk();

            }
        }

        private float CalculateDamage(float damage, float damageModifier)
        {
            return damage * damageModifier;
        }
    }
}

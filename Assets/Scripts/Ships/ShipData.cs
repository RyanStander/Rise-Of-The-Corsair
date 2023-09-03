using UnityEngine;

namespace Ships
{
    public class ShipData : MonoBehaviour
    {
        public ShipStats Stats;
        public float SailCurrentHealth { get; private set; }
        public float SailMaxHealth { get; private set; }
        public float HullCurrentHealth { get; private set; }
        public float HullMaxHealth { get; private set; }
        public ShipUpgrades Upgrades { get; private set; }
    }
}

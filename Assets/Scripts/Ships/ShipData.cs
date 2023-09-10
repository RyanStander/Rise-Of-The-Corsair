using UnityEngine;

namespace Ships
{
    public class ShipData : MonoBehaviour
    {
        public ShipStats Stats;
        public int SideCannonCount { get; private set; }
        public bool IsSunk { get; private set; }
        public int CurrentCrewCount { get; private set; }
        public ShipUpgrades Upgrades { get; private set; }

        public void ShipSunk()
        {
            IsSunk = true;
        }
    }
}

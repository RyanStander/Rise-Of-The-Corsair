using System.Collections.Generic;
using UnityEngine;

namespace Ships
{
    /// <summary>
    /// Holds data of all weapons on a chip and installs them
    /// </summary>
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private ShipData shipData;
        [SerializeField] private CannonPointHolder cannonPointHolder;
        [SerializeField] private GameObject presetCannonPrefab;
        private Dictionary<int , GameObject> cannonPointsDictionary = new Dictionary<int, GameObject>();

        private void OnValidate()
        {
            if (shipData == null)
                shipData = GetComponent<ShipData>();
            if (cannonPointHolder == null)
                cannonPointHolder = GetComponentsInChildren<CannonPointHolder>(true)[0];
        }
        
        private void Awake()
        {
            //Temporary, use presetCannonPrefab for all cannons
            for (int i = 0; i < cannonPointHolder.GetTotalCannonCount(); i++)
            {
                cannonPointsDictionary.Add(i, presetCannonPrefab);
            }
            
            foreach (KeyValuePair<int,GameObject> keyValuePair in cannonPointsDictionary)
            {
                cannonPointHolder.InstallCannon(keyValuePair.Key, keyValuePair.Value);
            }
        }

        public void InstallCannons(int cannonID, GameObject cannonPrefab)
        {
            if (cannonPointsDictionary.ContainsKey(cannonID))
            {
                cannonPointsDictionary[cannonID] = cannonPrefab;
                cannonPointHolder.InstallCannon(cannonID, cannonPrefab);
            }
        }
    }
}

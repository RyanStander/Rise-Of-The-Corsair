using System;
using UnityEngine;

namespace Ships
{
    public class ShipSteering : MonoBehaviour
    {
        [SerializeField] protected ShipData shipData;

        private void OnValidate()
        {
            GetReferences();
        }

        protected virtual void GetReferences()
        {
            if (shipData == null)
                shipData = GetComponent<ShipData>();
        }
    }
}

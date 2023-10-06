using UnityEngine;

namespace Ships
{
    public class ShipSway
    {
        private GameObject shipGameObject;

        private float maxSway = 0.1f;
        private float maxZRotation = 30f;
        private float currentSway;
        private float swaySpeed = 0.005f;

        public ShipSway(GameObject shipGameObject)
        {
            this.shipGameObject = shipGameObject;
        }

        public void UpdateSway(bool isLeft, bool isTurning, float currentTurnStrength)
        {
            var swayStrength = currentTurnStrength * swaySpeed;

            swayStrength = Mathf.Min(swayStrength, maxSway);

            if (isLeft & isTurning)
            {
                currentSway -= swayStrength;
            }
            else if (!isLeft & isTurning)
            {
                currentSway += swayStrength;
            }
            else
            {
                switch (currentSway)
                {
                    //If the ship is not turning, sway back to 0
                    case > 0:
                        currentSway -= swayStrength;
                        break;
                    case < 0:
                        currentSway += swayStrength;
                        break;
                }
            }

            var swayRotation = Vector3.forward * currentSway;

            shipGameObject.transform.Rotate(swayRotation);

            //Make it so that the ships z rotation is never more than 30 degrees and never less than -30 degrees
            var currentZRotation = shipGameObject.transform.rotation.eulerAngles.z;
            if (currentZRotation > 180)
                currentZRotation -= 360;

            currentZRotation = Mathf.Clamp(currentZRotation, -maxZRotation, maxZRotation);

            shipGameObject.transform.rotation = Quaternion.Euler(shipGameObject.transform.rotation.eulerAngles.x, shipGameObject.transform.rotation.eulerAngles.y, currentZRotation);

            
        }
    }
}

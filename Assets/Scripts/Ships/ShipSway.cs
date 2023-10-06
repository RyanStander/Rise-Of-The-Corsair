using UnityEngine;

namespace Ships
{
    public class ShipSway
    {
        private Transform targetTransform;

        private float rotationLimit = 10f;
        private float rotationSpeed = 30f;

        private Quaternion initialLocalRotation;
        private float currentRotation = 0.0f;

        public ShipSway(GameObject shipGameObject)
        {
            targetTransform = shipGameObject.transform;
            initialLocalRotation = shipGameObject.transform.rotation;
        }

        public void UpdateSway(bool isLeft, bool isTurning, float currentTurnStrength)
        {
            if (isTurning)
            {
                float rotationAmount = isLeft ? -rotationSpeed : rotationSpeed;
                currentRotation += rotationAmount * Time.deltaTime;

                // Ensure the local rotation stays within the limits.
                currentRotation = Mathf.Clamp(currentRotation, -rotationLimit, rotationLimit);

                Quaternion newLocalRotation = initialLocalRotation * Quaternion.Euler(0, 0, currentRotation);

                targetTransform.localRotation = newLocalRotation;
            }
            else
            {
                // Rotate back towards the initial local rotation.
                targetTransform.localRotation = Quaternion.RotateTowards(targetTransform.localRotation, initialLocalRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}

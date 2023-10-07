using UnityEngine;

namespace Ships
{
    public class ShipSway
    {
        private readonly Transform targetTransform;

        private const float RotationLimit = 10f;
        private const float RotationSpeed = 5f;

        private readonly Quaternion initialLocalRotation;
        private float currentRotation;

        public ShipSway(GameObject shipGameObject)
        {
            targetTransform = shipGameObject.transform;
            initialLocalRotation = shipGameObject.transform.rotation;
        }

        public void UpdateSway(bool isLeft, bool isTurning)
        {
            if (isTurning)
            {
                var rotationAmount = isLeft ? -RotationSpeed : RotationSpeed;
                currentRotation += rotationAmount * Time.deltaTime;

                // Ensure the local rotation stays within the limits.
                currentRotation = Mathf.Clamp(currentRotation, -RotationLimit, RotationLimit);

                var newLocalRotation = initialLocalRotation * Quaternion.Euler(0, 0, currentRotation);

                targetTransform.localRotation = newLocalRotation;
            }
            else
            {
                // Rotate back towards the initial local rotation.
                targetTransform.localRotation = Quaternion.RotateTowards(targetTransform.localRotation, initialLocalRotation, RotationSpeed * Time.deltaTime);

                //change current rotation to match the current z rotation
                currentRotation = targetTransform.localRotation.eulerAngles.z;

                //euler angles go from 0 to 360, so if it goes over 180, it will be negative
                if (currentRotation > 180)
                    currentRotation -= 360;
            }
        }
    }
}

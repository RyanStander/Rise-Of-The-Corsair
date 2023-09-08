using System;
using Ships.Enums;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Based on whether the follow camera is looking left or right of the ship it will swap to the port or starboard camera.
    /// If the player lets go of the aim button it will swap back to the follow camera.
    /// </summary>
    public class PlayerAiming : MonoBehaviour
    {
        [SerializeField] private CameraManager cameraManager;
        public ShipSide CurrentAimSide { get; private set; }
        private bool isAiming;

        private void OnValidate()
        {
            if (cameraManager == null)
                cameraManager = FindObjectsByType<CameraManager>(FindObjectsSortMode.None)[0];
            ;
        }

        private void FixedUpdate()
        {
            EnterAim();
            ExitAim();
            DetermineAimDirection();
        }

        private void EnterAim()
        {
            //If right click is pressed determine aim direction
            if (!Input.GetKeyDown(KeyCode.Mouse1))
                return;

            SwapToAimCamera();
            isAiming = true;
        }

        private void ExitAim()
        {
            //If right click is released swap back to the follow camera
            if (!Input.GetKeyUp(KeyCode.Mouse1))
                return;

            cameraManager.SwapToFollowCamera();
            isAiming = false;
        }

        private void DetermineAimDirection()
        {
            if (isAiming)
                return;

            //based on the position of the main camera and the ships position, determine whether the camera is to the left or right of the ship
            var cameraPosition = cameraManager.MainCamera.transform.position;
            var shipPosition = transform.position;
            var cameraDirection = cameraPosition - shipPosition;
            var shipDirection = transform.forward;
            var crossProduct = Vector3.Cross(cameraDirection, shipDirection);

            CurrentAimSide = crossProduct.y switch
            {
                > 0 => ShipSide.Starboard,
                < 0 => ShipSide.Port,
                _ => CurrentAimSide
            };
        }


        private void SwapToAimCamera()
        {
            switch (CurrentAimSide)
            {
                case ShipSide.Starboard:
                    cameraManager.SwapToStarboardCamera();
                    break;
                case ShipSide.Port:
                    cameraManager.SwapToPortCamera();
                    break;
                case ShipSide.Bow:
                    break;
                case ShipSide.Stern:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}

using System;
using Ships.Enums;
using UI;
using UI.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    /// <summary>
    /// Based on whether the follow camera is looking left or right of the ship it will swap to the port or starboard camera.
    /// If the player lets go of the aim button it will swap back to the follow camera.
    /// </summary>
    public class PlayerAiming : MonoBehaviour
    {
        [SerializeField] private CameraManager cameraManager;
        [SerializeField] private CursorManager cursorManager;
        public ShipSide CurrentAimSide { get; private set; }
        private bool isAiming;

        private void OnValidate()
        {
            if (cameraManager == null)
                cameraManager = FindObjectsByType<CameraManager>(FindObjectsSortMode.None)[0];

            if (cursorManager == null)
                cursorManager = FindObjectsByType<CursorManager>(FindObjectsSortMode.None)[0];
        }

        public void HandlePlayerAiming()
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

            cursorManager.SwapCursor(CursorTypes.Aim);

            SwapToAimCamera();
            isAiming = true;
        }

        private void ExitAim()
        {
            //If right click is released swap back to the follow camera
            if (!Input.GetKeyUp(KeyCode.Mouse1))
                return;

            cursorManager.SwapCursor(CursorTypes.Default);

            cameraManager.SwapToFollowCamera();
            isAiming = false;
        }

        private void DetermineAimDirection()
        {
            if (isAiming)
                return;

            var cameraPosition = cameraManager.MainCamera.transform.position;
            var shipPosition = transform.position;
            var cameraDirection = cameraPosition - shipPosition;
            var shipDirection = transform.forward;

            //angle between where the ship is facing and where the camera currently is, signed around the Y axis
            var angle = Vector3.SignedAngle(shipDirection, cameraDirection, Vector3.up);

            CurrentAimSide = angle switch
            {
                > -45f and <= 45f => ShipSide.Stern,
                > 45f and <= 135f => ShipSide.Starboard,
                > -135f and <= -45f => ShipSide.Port,
                _ => ShipSide.Bow
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

using System;
using Cinemachine;
using UnityEngine;

namespace Player
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] public Camera MainCamera;
        [SerializeField] private CinemachineVirtualCamera followCamera;
        [SerializeField] private CinemachineVirtualCamera portCamera;
        [SerializeField] private CinemachineVirtualCamera starboardCamera;

        private void OnValidate()
        {
            if(MainCamera == null)
                MainCamera = Camera.main;
        }

        public void SwapToFollowCamera()
        {
            followCamera.Priority = 10;
            portCamera.Priority = 0;
            starboardCamera.Priority = 0;
        }

        public void SwapToPortCamera()
        {
            followCamera.Priority = 0;
            portCamera.Priority = 10;
            starboardCamera.Priority = 0;
        }

        public void SwapToStarboardCamera()
        {
            followCamera.Priority = 0;
            portCamera.Priority = 0;
            starboardCamera.Priority = 10;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

namespace Misc
{
    [ExecuteInEditMode]
    public class FitToWaterSurface : MonoBehaviour
    {
        [SerializeField] private WaterSurface targetSurface = null;
        [SerializeField] private float yOffset = 0.0f;

        [Tooltip("Front of ship")] [SerializeField]
        private GameObject bowPoint;

        private Vector3 targetBowPosition;

        [Tooltip("Back of ship")] [SerializeField]
        private GameObject sternPoint;

        private Vector3 targetSternPosition;

        [Tooltip("Left of ship")] [SerializeField]
        private GameObject portPoint;

        private Vector3 targetPortPosition;

        [Tooltip("Right of ship")] [SerializeField]
        private GameObject starboardPoint;

        private Vector3 targetStarboardPosition;
        [SerializeField] private float dampStrength = 2.0f;

        // Internal search params
        private WaterSearchParameters searchParameters = new();
        private WaterSearchResult searchResult;

        private void Update()
        {
            if (targetSurface == null)
                return;

            targetBowPosition = GetProjectedWaterPosition(bowPoint.transform.position);
            targetSternPosition = GetProjectedWaterPosition(sternPoint.transform.position);
            targetPortPosition = GetProjectedWaterPosition(portPoint.transform.position);
            targetStarboardPosition = GetProjectedWaterPosition(starboardPoint.transform.position);

            //average all points for target vert position
            float averageY = (targetBowPosition.y + targetSternPosition.y + targetPortPosition.y +
                              targetStarboardPosition.y) / 4.0f;

            //get the rotation target with a cross product
            Vector3 foreAftDirection = targetBowPosition - targetSternPosition;
            Vector3 sideToSideDirection = targetPortPosition - targetStarboardPosition;

            Vector3 rotationTarget = Vector3.Cross(sideToSideDirection, foreAftDirection).normalized;

            //damp to target position
            Vector3 targetPosition = new Vector3(transform.position.x, averageY + yOffset, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * dampStrength);

            //damp to target rotation, need to preserve the yaw and heading
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, rotationTarget);
            Quaternion yRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            Quaternion finalRotation = yRotation * targetRotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, finalRotation, Time.deltaTime * dampStrength);
        }

        private Vector3 GetProjectedWaterPosition(Vector3 position)
        {
            //reset parameters
            searchParameters = new WaterSearchParameters();
            searchResult = new WaterSearchResult();

            // Build the search parameters
            searchParameters.startPositionWS = searchResult.candidateLocationWS;
            searchParameters.targetPositionWS = position;
            searchParameters.error = 0.01f;
            searchParameters.maxIterations = 8;

            // Do the search
            if (targetSurface.ProjectPointOnWaterSurface(searchParameters, out searchResult))
            {
                return searchResult.projectedPositionWS;
            }

            return position;
        }
    }
}

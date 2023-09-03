using Events;
using UnityEngine;

namespace TimeAndSeasons
{
    public class TimeDisplayRotator : MonoBehaviour
    {
        private float timeStrength;
        [SerializeField]private RectTransform objectToRotate;
        //Determines if the rotation should increase or decrease
        [SerializeField] private bool approachingDaytime;
        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventIdentifiers.SendTimeStrength, OnReceiveTimeStrength);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventIdentifiers.SendTimeStrength, OnReceiveTimeStrength);
        }

        private void Update()
        {
            float rotationValue;

            if (approachingDaytime)
            {
                rotationValue = 180+(180 * timeStrength);
                if (timeStrength >0.99999f)
                    approachingDaytime = false;

            }
            else
            {
                rotationValue = 180 - (180 * timeStrength);
                if (timeStrength <0.00001f)
                    approachingDaytime = true;
            }
            //Debug.Log(rotationValue);
            objectToRotate.rotation=Quaternion.Euler(0,0, rotationValue);

        }

        private void OnReceiveTimeStrength(EventData eventData)
    {
        if (!eventData.IsEventOfType(out SendTimeStrength sendTimeStrength)) return;
        timeStrength = sendTimeStrength.TimeStrength;
    }
    }
}

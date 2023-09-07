using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 5f;

    private float timeStamp;

    private void Start()
    {
        timeStamp = Time.time + timeToDestroy;
    }

    private void Update()
    {
        if (timeStamp <= Time.time)
        {
            Destroy(gameObject);
        }
    }
}

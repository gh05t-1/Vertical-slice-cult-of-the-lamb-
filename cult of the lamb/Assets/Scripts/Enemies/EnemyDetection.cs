using UnityEngine;
using System;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] private float detectingZone = 10f;
    [SerializeField] private float dashZone = 3f;

    public event Action<Transform> OnPlayerDetected;
    public event Action<Transform> OnDashZoneDetected;
    public event Action OnPlayerLost;

    private Transform detectedPlayer;
    private bool playerVisible = false;

    private void Update()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null) return;

        float distance = Vector3.Distance(transform.position, playerObj.transform.position);

        if (distance <= detectingZone)
        {
            if (!playerVisible)
            {
                playerVisible = true;
                detectedPlayer = playerObj.transform;
                OnPlayerDetected?.Invoke(detectedPlayer);
            }

            if (distance <= dashZone)
            {
                OnDashZoneDetected?.Invoke(detectedPlayer);
            }
        }
        else
        {
            if (playerVisible)
            {
                playerVisible = false;
                detectedPlayer = null;
                OnPlayerLost?.Invoke();
            }
        }
    }
}

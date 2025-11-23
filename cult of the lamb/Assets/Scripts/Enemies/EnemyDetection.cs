using UnityEngine;
using System;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] private float detectingZone = 10f;
    [SerializeField] private float dashZone = 3f;
    private float dashZoneMin = 1f;

    public event Action<Transform> OnPlayerDetected;
    public event Action<Transform> OnDashZoneDetected;
    public event Action<Transform> OnDashing;
    public event Action<Transform> OnDashingStop;
    public event Action<Transform> OnChaseing;
    public event Action<Transform> OnChaseingStop;
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
                if (distance >= dashZoneMin)
                { 
                    OnDashZoneDetected?.Invoke(detectedPlayer);
                }
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnDashing?.Invoke(other.transform);
            OnChaseing?.Invoke(other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnDashingStop?.Invoke(other.transform);
            OnChaseingStop?.Invoke(other.transform);
        }
    }
}
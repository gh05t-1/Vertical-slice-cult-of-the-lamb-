using UnityEngine;
using System;

public class EnemyPatrol : EnemyMovement
{
    [SerializeField] private float arrivalThreshold = 0.1f;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float waitTime = 0.1f;
    [SerializeField] private Transform startPoint;
    private bool isWaiting = false;
    private int targetPoint;
    private bool isPatrolling = true;

    private void Start()
    {
        targetPoint = 0;
        OnStartPatrol += () => isPatrolling = true;
        OnStopPatrol += () => isPatrolling = false;

        if (startPoint != null)
            transform.position = startPoint.position;
    }

    private void Update()
    {
        if (EnemyMovement.BlockAllMovement) return;

        if (!isPatrolling || isWaiting || patrolPoints.Length == 0)
            return;

        if (Vector3.Distance(transform.position, patrolPoints[targetPoint].position) <= arrivalThreshold)
        {
            targetPoint = (targetPoint + 1) % patrolPoints.Length;
            StartCoroutine(WaitRoutine());
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            patrolPoints[targetPoint].position,
            speed * Time.deltaTime
        );
    }

    private System.Collections.IEnumerator WaitRoutine()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
    }
}

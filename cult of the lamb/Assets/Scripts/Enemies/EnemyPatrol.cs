using UnityEngine;

public class EnemyPatrol : EnemyMovement
{
    [SerializeField] private float arrivalThreshold = 0.1f;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float waitTime = 0.1f;

    private bool isWaiting = false;
    private int targetPoint;
    private bool isPatrolling = true;
    private EnemyChase enemyChase;
    private EnemyDash enemyDash;

    private void Start()
    {
        enemyChase = GetComponent<EnemyChase>();
        enemyDash = GetComponent<EnemyDash>();
        EnemyHealth health = GetComponent<EnemyHealth>();
        health.OnEnemyDeath += stopAllEvents;
        OnStartPatrol += () => isPatrolling = true;
        OnStopPatrol += () => isPatrolling = false;

        FindNearestPatrolPoint();
    }

    private void Update()
    {
        if (!isPatrolling || isWaiting || patrolPoints.Length == 0) return;
        if (enemyChase != null && enemyChase.IsChasing()) return;
        if (enemyDash != null && (enemyDash.IsDashing() || enemyDash.IsLocked())) return;

        Vector3 moveDir = (patrolPoints[targetPoint].position - transform.position).normalized;
        UpdateFlip(moveDir);

        if (Vector3.Distance(transform.position, patrolPoints[targetPoint].position) <= arrivalThreshold)
        {
            targetPoint = (targetPoint + 1) % patrolPoints.Length;
            StartCoroutine(WaitRoutine());
        }

        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
    }

    private System.Collections.IEnumerator WaitRoutine()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
    }

    public void FindNearestPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        float nearestDistance = float.MaxValue;
        int nearestIndex = 0;

        for (int i = 0; i < patrolPoints.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, patrolPoints[i].position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestIndex = i;
            }
        }

        targetPoint = nearestIndex;
    }

    private void stopAllEvents()
    {
        EnemyHealth health = GetComponent<EnemyHealth>();
        health.OnEnemyDeath -= stopAllEvents;
        OnStartPatrol -= () => isPatrolling = true;
        OnStopPatrol -= () => isPatrolling = false;
    }
}

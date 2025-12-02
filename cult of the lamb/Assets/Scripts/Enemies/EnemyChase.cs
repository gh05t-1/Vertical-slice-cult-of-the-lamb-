using UnityEngine;

public class EnemyChase : EnemyMovement
{
    private Transform player;
    private bool isChasing = false;
    private EnemyDash enemyDash;
    private float originalSpeed;

    private void Start()
    {
        EnemyDetection detector = GetComponent<EnemyDetection>();
        EnemyHealth health = GetComponent<EnemyHealth>();
        health.OnEnemyDeath += stopAllEvents;
        detector.OnPlayerDetected += StartChasingFromZone;
        detector.OnPlayerLost += StopChasingFromZone;
        enemyDash = GetComponent<EnemyDash>();
        detector.OnChaseing += StopChasingFromCollider;
        detector.OnChaseingStop += StartChasingFromCollider;
        originalSpeed = speed;
    }

    private void Update()
    {
        if (!isChasing || player == null) return;
        if (enemyDash != null && (enemyDash.IsDashing() || enemyDash.IsLocked())) return;

        Vector3 playerPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        Vector3 moveDir = (playerPosition - transform.position).normalized;

        UpdateFlip(moveDir);
        transform.position += moveDir * speed * Time.deltaTime;
    }

    private void StartChasingFromZone(Transform p)
    {
        player = p;
        isChasing = true;
        TriggerStopPatrol();
        TriggerStartChase();
    }

    private void StopChasingFromZone()
    {
        player = null;
        isChasing = false;
        TriggerStopChase();

        EnemyPatrol patrol = GetComponent<EnemyPatrol>();
        if (patrol != null)
        {
            patrol.FindNearestPatrolPoint();
        }

        TriggerStartPatrol();
    }

    private void stopAllEvents()
    {
        EnemyDetection detector = GetComponent<EnemyDetection>();
        EnemyHealth health = GetComponent<EnemyHealth>();
        health.OnEnemyDeath -= stopAllEvents;
        detector.OnPlayerDetected -= StartChasingFromZone;
        detector.OnPlayerLost -= StopChasingFromZone;
        detector.OnChaseing -= StopChasingFromCollider;
        detector.OnChaseingStop -= StartChasingFromCollider;
    }
    private void StopChasingFromCollider(Transform p)
    {
        speed = 0f;
    }

    private void StartChasingFromCollider(Transform p)
    {
        speed = originalSpeed;
    }

    public bool IsChasing() => isChasing;
}

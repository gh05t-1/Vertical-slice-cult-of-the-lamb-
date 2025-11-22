using UnityEngine;

public class EnemyChase : EnemyMovement
{
    private Transform player;
    private bool isChasing = false;

    private void Start()
    {
        EnemyDetection detector = GetComponent<EnemyDetection>();
        detector.OnPlayerDetected += StartChasing;
        detector.OnPlayerLost += StopChasing;
    }

    private void Update()
    {
        if (EnemyMovement.BlockAllMovement) return;

        if (!isChasing || player == null) return;

        Vector3 moveDir = (player.position - transform.position).normalized;
        transform.position += moveDir * speed * Time.deltaTime;
    }


    private void StartChasing(Transform p)
    {
        player = p;
        isChasing = true;
        TriggerStopPatrol();
        TriggerStartChase();
    }

    private void StopChasing()
    {
        player = null;
        isChasing = false;
        TriggerStopChase();
        TriggerStartPatrol();
    }
}

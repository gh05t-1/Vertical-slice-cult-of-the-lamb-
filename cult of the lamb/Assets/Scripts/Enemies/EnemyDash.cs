using UnityEngine;

public class EnemyDash : EnemyMovement
{
    [SerializeField] private float dashSpeedMultiplier = 6f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private float lockDelay = 0.15f;

    private float lastDashTime = -Mathf.Infinity;
    private bool isDashing = false;

    private void Start()
    {
        EnemyDetection detector = GetComponent<EnemyDetection>();
        detector.OnDashZoneDetected += TryDash;
    }

    private void TryDash(Transform player)
    {
        if (player == null) return;
        if (isDashing) return;
        if (Time.time - lastDashTime < dashCooldown) return;

        StartCoroutine(DashRoutine(player));
    }

    private System.Collections.IEnumerator DashRoutine(Transform player)
    {
        isDashing = true;

        EnemyMovement.BlockAllMovement = true;

        Vector3 lockedPosition = player.position;

        yield return new WaitForSeconds(lockDelay);

        Vector3 dashDirection = (lockedPosition - transform.position).normalized;

        float timer = 0f;

        while (timer < dashDuration)
        {
            transform.position += dashDirection * speed * dashSpeedMultiplier * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        EnemyMovement.BlockAllMovement = false;

        lastDashTime = Time.time;
        isDashing = false;
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class EnemyDash : EnemyMovement
{
    [SerializeField] private float dashSpeedMultiplier = 6f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private float lockDelay = 0.15f;
    private float lastDashTime = -Mathf.Infinity;
    private bool isDashing = false;
    private bool isLocked = false;

    private void Start()
    {
        EnemyDetection detector = GetComponent<EnemyDetection>();
        detector.OnDashZoneDetected += TryDash;
        detector.OnDashing += StopDashing;
        detector.OnDashingStop += StartDshing;
    }

    private void TryDash(Transform player)
    {
        if (player == null) return;
        if (isDashing || isLocked) return;
        if (Time.time - lastDashTime < dashCooldown) return;
        StartCoroutine(DashRoutine(player));
    }

    private void StopDashing(Transform player)
    {
        dashDuration = 0f;
    }

    private void StartDshing(Transform player)
    {
        dashDuration = 0.2f;
    }   

    private System.Collections.IEnumerator DashRoutine(Transform player)
    {
        isDashing = true;
        isLocked = true;

        Vector3 playerPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        Vector3 lockedPosition = playerPosition;

        yield return new WaitForSeconds(lockDelay);

        isLocked = false;
        Vector3 dashDirection = (lockedPosition - transform.position).normalized;

        float timer = 0f;

        while (timer < dashDuration)
        {
            transform.position += dashDirection * speed * dashSpeedMultiplier * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        lastDashTime = Time.time;
        isDashing = false;
    }

    public bool IsDashing() => isDashing;
    public bool IsLocked() => isLocked;
}
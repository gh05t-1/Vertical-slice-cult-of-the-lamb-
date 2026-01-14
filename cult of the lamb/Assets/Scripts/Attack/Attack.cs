
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackCooldown = 0.5f;
    public int attackDamage = 20;
    public float attackRange = 0.75f;
    public LayerMask enemyLayers;

    [Header("References")]
    public Animator animator;
    public Camera mainCamera; // Assign your main camera in the Inspector

    private bool canAttack = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack) // Left click for attack
        {
            StartCoroutine(PerformAttack());
        }
    }

    private System.Collections.IEnumerator PerformAttack()
    {
        canAttack = false;

        // Trigger attack animation
        animator.SetTrigger("Attack");

        // Wait for the animation's hit frame (adjust timing to match your animation)
        yield return new WaitForSeconds(0.2f);

        // Get cursor position in world space
        Vector3 cursorWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        cursorWorldPos.z = 0f; // Ensure it's on the 2D plane

        // Detect enemies in range of the cursor
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(cursorWorldPos, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }

        // Wait for cooldown
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    void OnDrawGizmosSelected()
    {
        if (mainCamera == null) return;

        // Draw gizmo at cursor position for debugging
        Vector3 cursorWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        cursorWorldPos.z = 0f;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(cursorWorldPos, attackRange);
    }
}

using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float currentHealth;
    [SerializeField] private float damagePerSecond = 5f;
    [SerializeField] private Image enemyHealthBar;
    [SerializeField] private float reduceSpeed = 2f;

    private float targetFillAmount = 1f;

    public event Action OnEnemyDeath;

    private void Start()
    {
        currentHealth = maxHealth;
        targetFillAmount = 1f;
        enemyHealthBar.fillAmount = 1f;
    }

    private void Update()
    {
        TakeDamage(damagePerSecond * Time.deltaTime);
        enemyHealthBar.fillAmount = math.lerp(
            enemyHealthBar.fillAmount,
            targetFillAmount,
            Time.deltaTime * reduceSpeed
        );
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = math.max(currentHealth, 0);
        targetFillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnEnemyDeath?.Invoke();
        Destroy(gameObject);
    }
}

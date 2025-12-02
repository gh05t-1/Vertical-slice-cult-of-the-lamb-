using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float currentHealth;
    [SerializeField] private float damagePerSecond = 5f;
    public event Action OnEnemyDeath;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        TakeDamage(damagePerSecond * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
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

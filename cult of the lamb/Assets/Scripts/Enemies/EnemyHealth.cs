using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float damagePerSecond = 5f;
    [SerializeField] private float damageDelay = 5f;
    [SerializeField] private Slider healthBar;
    [SerializeField] private float reduceSpeed = 5f;


    private float currentHealth;
    private float targetValue = 1f;
    private bool takingDamage;

    public event Action OnEnemyDeath;


    private void Start()
    {
        currentHealth = maxHealth;

        healthBar.minValue = 0f;
        healthBar.maxValue = 1f;
        healthBar.value = 1f;
        healthBar.gameObject.SetActive(false);

        StartCoroutine(DamageAfterDelay());
    }

    private void Update()
    {
        if (takingDamage)
        {
            if (!healthBar.gameObject.activeSelf)
                healthBar.gameObject.SetActive(true);

            ApplyDamage(damagePerSecond * Time.deltaTime);
        }


        healthBar.value = math.lerp(
            healthBar.value,
            targetValue,
            Time.deltaTime * reduceSpeed
        );
    }

    private IEnumerator DamageAfterDelay()
    {
        yield return new WaitForSeconds(damageDelay);
        if (damagePerSecond > 0f)
            takingDamage = true;
    }

    public void ApplyDamage(float damage)
    {

        currentHealth -= damage;

        if (currentHealth <= 0f)
            Die();

    }

    private void Die()
    {
        OnEnemyDeath?.Invoke();
        Destroy(gameObject);
    }
}


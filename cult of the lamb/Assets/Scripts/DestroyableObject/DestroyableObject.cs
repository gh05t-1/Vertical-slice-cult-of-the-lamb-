using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    [SerializeField] private int ObjectHealth = 50;
    [SerializeField] private int damage = 10;
    private int currentHealth;
    void Start()
    {
        currentHealth = ObjectHealth;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyDamage(damage);
            Debug.Log("Object hit by Player, applied damage: " + damage);
        }
    }

    private void ApplyDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Object Health: " + currentHealth);
    }
}

using NUnit.Framework;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] float _damage;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Hit");
            GetComponent<EnemyHealth>().ApplyDamage(_damage);
        }
    }
}

using NUnit.Framework;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] float _damage;
    bool _inTrigger;
    [SerializeField] EnemyHealth _enemyHealth;
    [SerializeField] private Animator _animator;


    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Mouse0) && _inTrigger)
        {

            _enemyHealth.ApplyDamage(_damage);
            _animator.SetBool("isAttacking", true);
            _inTrigger = false;

        }

        if (_inTrigger == false)
        {
            _animator.SetBool("isAttacking", false);
        }

    }
    public void OnTriggerEnter(Collider other)
    {if (other.CompareTag("Enemy"))
        {
            _inTrigger = true;

            
        }
    }



    public void OnTriggerExit(Collider other)
    {
        _inTrigger = false;

    }
}

using NUnit.Framework;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] float _damage;
    bool _inTrigger;
 


  

    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Mouse0) && _inTrigger)
        {
            Debug.Log("Hit");
            GetComponent<EnemyHealth>().ApplyDamage(_damage);
        }
    }
    public void OnTriggerEnter(Collider other)
    {if (other.CompareTag("Attack"))
        {
            _inTrigger = true;

            
        }
    }



    public void OnTriggerExit(Collider other)
    {
        _inTrigger = false;

    }
}

using System.Collections;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] float _rollSpeed;
    [SerializeField] float _rollTime;
    bool _dodging = true;


    [SerializeField] LayerMask _playerLayer;
    [SerializeField] LayerMask _enemyLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && _dodging)
        {
           StartCoroutine(Roll());
        }
           
    }
    private IEnumerator Roll()
    {
        _dodging = false;
        Physics.IgnoreLayerCollision(_playerLayer, _enemyLayer, _dodging);
        _rb.AddForce(_rollSpeed * _rb.transform.right, ForceMode.Impulse);
        
        yield return new WaitForSeconds(_rollTime);
        _dodging = true;
    }
}

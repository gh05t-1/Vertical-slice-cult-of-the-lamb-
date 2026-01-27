using System.Collections;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] float _rollSpeed = 10f;
    [SerializeField] float _rollTime = 0.2f;

   // Vector3 _moveDirection;
    bool _dodging = true;

    [SerializeField] private Animator _animator;



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
        if (!_dodging)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && _dodging)
        {
           StartCoroutine(Roll());
        }

           
    }

    private IEnumerator Roll()
    {
        // Play dodge animation
        _animator.SetBool("isDashing", true);
        _dodging = false;
        // Get input for movement
        Vector3 _movement = Vector3.zero;
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.z = Input.GetAxisRaw("Vertical");

        // Normalize diagonal movement to prevent faster diagonal speed
        _movement = _movement.normalized;

        Physics.IgnoreLayerCollision(_playerLayer, _enemyLayer, _dodging);
         _rb.AddForce(_movement * _rollSpeed * Time.deltaTime, ForceMode.Impulse);
        //_rb.linearVelocity = new Vector3(_moveDirection.x * _rollSpeed, 0, _moveDirection.z * _rollSpeed);
        // _rb.linearVelocity = new Vector3(transform.localScale.x * _rollSpeed, 0, transform.localScale.z *_rollSpeed)
        yield return new WaitForSeconds(_rollTime);
        // Stop dodge animation
        _animator.SetBool("isDashing", false);
        _rb.linearVelocity = new Vector3(0, 0, 0);
        _dodging = true;
    }
}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float _speed = 10f;
    private Rigidbody _rb;
    private Vector3 _movement;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input for movement
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.z = Input.GetAxisRaw("Vertical");

        // Normalize diagonal movement to prevent faster diagonal speed
        _movement = _movement.normalized;
    }

    void FixedUpdate()
    {
        // Apply movement using physics
        _rb.MovePosition(_rb.position + _movement * _speed * Time.fixedDeltaTime);
    }
}

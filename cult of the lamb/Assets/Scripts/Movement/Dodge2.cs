using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dodge2 : MonoBehaviour
{

    
    public Rigidbody rb;
 


    public float moveSpeed = 10.0f;
    public float dashSpeed = 32.0f;
    public float dashDecaySpeed = 64.0f;
    public float dashPartcileRate = 2.0f;



    Vector3 _moveInput;
    bool _dashing;





    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !_dashing)
        {
            _dashing = true;

            rb.linearVelocity = transform.forward.normalized * dashSpeed;
        }
    }
   

    void FixedUpdate()
    {
        var inputVelocity = _moveInput * moveSpeed;
        transform.LookAt(transform.position + inputVelocity);
        if (_dashing)
        {
            var sqrMagnitude = rb.linearVelocity.sqrMagnitude;
            if (sqrMagnitude > inputVelocity.sqrMagnitude && sqrMagnitude > 0.5f)
            {
                rb.linearVelocity -= dashDecaySpeed * Time.fixedDeltaTime * rb.linearVelocity.normalized;
                return;
            }
            _dashing = false;

        }
        rb.linearVelocity = inputVelocity;
    }
}

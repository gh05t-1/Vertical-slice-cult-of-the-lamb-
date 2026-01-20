
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    void Update()
    {
        PlayerMovement movement = GetComponent<PlayerMovement>();
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            _animator.SetBool("isWalking", true);
            Flip(Input.GetAxis("Horizontal"));
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }

    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (horizontal < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
}


using System.Collections;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    Rigidbody _rb;
    bool _dodging = false;
    [SerializeField] float _rollSpeed;

    [SerializeField] LayerMask _playerLayer;
    [SerializeField] LayerMask _enemyLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("Dodge"))
        {

        }
    }
    private IEnumerator Roll()
    {
        _dodging = true;
        Physics.IgnoreLayerCollision(_playerLayer, _enemyLayer, _dodging);
    }
}

using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _speed;

    Vector3 _offset;

    private void Awake()
    {
        _offset = _target.position - transform.position;
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position - _offset, _speed * Time.deltaTime);
    }
}

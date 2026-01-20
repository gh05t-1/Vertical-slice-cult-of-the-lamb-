using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float coinFallingSpeed = 2f;
    [SerializeField] private float coinRisingSpeed = 2f;
    [SerializeField] private float fallingTime = 1f;
    [SerializeField] private float risingTime = 0.5f;
    [SerializeField] private Vector3 riseingPoint = new (0f, 2f, 0f);

    void Update()
    {
        if (risingTime > 0f)
        {
            transform.position += riseingPoint * coinRisingSpeed * Time.deltaTime;
            risingTime -= Time.deltaTime;
            return;
        }

        if (fallingTime > 0f)
        {
            transform.position += Vector3.down * coinFallingSpeed * Time.deltaTime;
            fallingTime -= Time.deltaTime;
            return;
        }

        coinFallingSpeed = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        { 
            Destroy(gameObject);
        }

    }
}


using System.Collections.Generic;
using UnityEngine;

public class EnemyParts : MonoBehaviour
{
    [SerializeField] private GameObject[] bodyPartPrefabs;
    [SerializeField] private int bodyLength = 4;
    [SerializeField] private float gap = 0.3f;
    [SerializeField] private float baseY = 0f;

    private List<GameObject> bodyParts = new List<GameObject>();

    private void Start()
    {
        EnemyHealth health = GetComponent<EnemyHealth>();
        if (health != null)
        {
            health.OnEnemyDeath += () =>
            {
                foreach (var part in bodyParts)
                {
                    if (part != null) Destroy(part);
                }
                bodyParts.Clear();
            };
        }

        for (int i = 0; i < bodyLength; i++)
        {
            GameObject part = Instantiate(bodyPartPrefabs[i % bodyPartPrefabs.Length]);
            Vector3 pos = transform.position;
            pos.y = baseY;
            part.transform.position = pos;
            part.transform.rotation = Quaternion.Euler(0, 180, 0);
            bodyParts.Add(part);
        }
    }

    private void Update()
    {
        for (int i = 0; i < bodyParts.Count; i++)
        {
            Vector3 target;

            if (i == 0)
            {
                target = transform.position;
            }
            else
            {
                target = bodyParts[i - 1].transform.position;
            }

            Vector3 dir = bodyParts[i].transform.position - target;
            float dist = dir.magnitude;
            float diff = dist - gap;

            if (Mathf.Abs(diff) > 0.001f)
            {
                Vector3 correction = dir.normalized * diff;
                bodyParts[i].transform.position -= correction;
            }

            Vector3 p = bodyParts[i].transform.position;
            p.y = baseY;
            bodyParts[i].transform.position = p;

            bodyParts[i].transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}

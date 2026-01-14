using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private GameObject[] enemiesOnScene;
    [SerializeField] private int enemiesToDefeat;

    public event System.Action OnAllEnemiesDefeated;
    private readonly List<EnemyHealth> subscribedHealths = new List<EnemyHealth>();

    private void Start()
    {
        if (enemiesOnScene != null)
        {
            for (int i = 0; i < enemiesOnScene.Length; i++)
            {
                var go = enemiesOnScene[i];
                if (go == null)
                    continue;

                var health = go.GetComponent<EnemyHealth>();
                if (health != null)
                {
                    health.OnEnemyDeath += countEnemiesDown;
                    subscribedHealths.Add(health);
                    enemiesToDefeat++;
                }
            }
        }

    }

    private void OnDestroy()
    {
        UnsubscribeAll();
    }

    private void countEnemiesDown()
    {
        enemiesToDefeat--;
        if (enemiesToDefeat <= 0)
        {
            OnAllEnemiesDefeated?.Invoke();
            UnsubscribeAll();
            Debug.Log("All enemies defeated!");
        }
    }

    private void UnsubscribeAll()
    {
        for (int i = 0; i < subscribedHealths.Count; i++)
        {
            var h = subscribedHealths[i];
            if (h != null)
                h.OnEnemyDeath -= countEnemiesDown;
        }
        subscribedHealths.Clear();
    }
}

using UnityEngine;

public class ChestOpening : MonoBehaviour
{
    [SerializeField] private GameObject chestSpawner;
    [SerializeField] private GameObject chestPlace;
    [SerializeField] private Coin coinPrefab;
    [SerializeField] private float coinSpawnDelay = 1f;
    [SerializeField] private int coinSpawnCount = 1;

    private bool hasOpened = false;
    private float delayTimer;
    private int coinsSpawned = 0;

    void Start()
    {
        EnemyCounter counter = GetComponent<EnemyCounter>();
        if (counter != null)
        {
            counter.OnAllEnemiesDefeated += OpenChest;
        }
        chestSpawner.SetActive(true);
    }

    void Update()
    {
        if (!hasOpened) return;

        if (delayTimer > 0f)
        {
            delayTimer -= Time.deltaTime;
            return;
        }

        if (coinsSpawned < coinSpawnCount)
        {
            Instantiate(coinPrefab, chestPlace.transform.position, Quaternion.identity);
            coinsSpawned++;
        }
    }

    private void OpenChest()
    {
        Instantiate(chestSpawner, chestPlace.transform.position, Quaternion.identity);
        hasOpened = true;
        delayTimer = coinSpawnDelay;
        coinsSpawned = 0;
    }
}

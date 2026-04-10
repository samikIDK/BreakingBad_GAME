using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject enemyPrefab;
    public float spawnRadius = 10f;
    public int maxEnemies = 50;

    [Header("Difficulty")]
    public float initialSpawnInterval = 3f;
    public float minSpawnInterval = 0.5f;
    public float difficultyIncreaseInterval = 30f;
    public float enemyHealthMultiplier = 1.1f;

    private float spawnTimer;
    private float difficultyTimer;
    private float currentSpawnInterval;
    private float currentHealthMultiplier = 1f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentSpawnInterval = initialSpawnInterval;
    }

    void Update()
    {
        // Difficulty timer — každých 30 sekund hra těžší
        difficultyTimer += Time.deltaTime;
        if (difficultyTimer >= difficultyIncreaseInterval)
        {
            IncreaseDifficulty();
            difficultyTimer = 0f;
        }

        // Spawn timer
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentSpawnInterval && CountEnemies() < maxEnemies)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector2 spawnPos = (Vector2)player.position + randomDirection * spawnRadius;

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        // Nastaví HP podle aktuálního multiplieru
        HealthSystem health = enemy.GetComponent<HealthSystem>();
        if (health != null)
        {
            health.maxHealth *= currentHealthMultiplier;
        }
    }

    void IncreaseDifficulty()
    {
        // Zrychlí spawn
        currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - 0.3f);
        
        // Zvýší HP nepřátel
        currentHealthMultiplier *= enemyHealthMultiplier;

        Debug.Log("Difficulty increased! Spawn interval: " + currentSpawnInterval);
    }

    int CountEnemies()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
}
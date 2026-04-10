using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject enemyPrefab;
    public float spawnInterval = 3f;
    public int maxEnemies = 20;
    public float spawnRadius = 10f;

    private float timer;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && CountEnemies() < maxEnemies)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        // Spawne nepřítele na náhodné místo kolem hráče
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector2 spawnPos = (Vector2)player.position + randomDirection * spawnRadius;

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    int CountEnemies()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
}
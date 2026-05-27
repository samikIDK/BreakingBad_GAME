using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    public float bulletSpeed = 8f;
    public float shootRange = 8f;
    public float bulletDamage = 20f;

    private float timer;

    void Start()
    {
        string character = PlayerPrefs.GetString("SelectedCharacter", "Walter");
        int level = character == "Walter" ?
            PlayerPrefs.GetInt("WalterLevel", 1) :
            PlayerPrefs.GetInt("JesseLevel", 1);

        bulletDamage *= 1f + (level - 1) * 0.05f;
        fireRate *= 1f + (level - 1) * 0.03f;

        // Aplikuj ingame upgrady
        if (GameManager.Instance != null)
        {
            bulletDamage *= GameManager.Instance.damageMultiplier;
            fireRate *= GameManager.Instance.attackSpeedMultiplier;
            shootRange *= GameManager.Instance.shootRangeMultiplier;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f / fireRate)
        {
            GameObject nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null)
            {
                Shoot(nearestEnemy.transform);
                timer = 0f;
            }
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float minDistance = shootRange;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = enemy;
            }
        }
        return nearest;
    }

    void Shoot(Transform target)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
        bullet.GetComponent<Bullet>().damage = bulletDamage;
    }
}
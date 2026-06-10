using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Bullets")]
    public GameObject bulletPrefabWalter;
    public GameObject bulletPrefabJesse;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    public float bulletSpeed = 8f;
    public float shootRange = 8f;
    public float bulletDamage = 20f;
    public bool doubleShot = false;

    private string character;
    private float timer;
    private float burstTimer = 0f;
    private float burstCooldown = 0.8f;
    private float burstDelay = 0.1f;
    private int burstCount = 0;
    private int burstMax = 3;
    private bool isBursting = false;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        character = PlayerPrefs.GetString("SelectedCharacter", "Walter");
        int level = character == "Walter" ?
            PlayerPrefs.GetInt("WalterLevel", 1) :
            PlayerPrefs.GetInt("JesseLevel", 1);

        if (character == "Walter")
        {
            bulletDamage = 40f;
            fireRate = 0.8f;
            bulletPrefab = bulletPrefabWalter;
        }
        else
        {
            bulletDamage = 15f;
            fireRate = 3f;
            bulletPrefab = bulletPrefabJesse;
        }

        bulletDamage *= 1f + (level - 1) * 0.05f;
        fireRate *= 1f + (level - 1) * 0.03f;

        if (GameManager.Instance != null)
        {
            bulletDamage *= GameManager.Instance.damageMultiplier;
            fireRate *= GameManager.Instance.attackSpeedMultiplier;
            bulletSpeed *= GameManager.Instance.bulletSpeedMultiplier;
            doubleShot = GameManager.Instance.doubleShot;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (character == "Walter")
        {
            if (timer >= 1f / fireRate)
            {
                Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
                ShootShotgun(direction);
                timer = 0f;
            }
        }
        else
        {
            if (!isBursting && timer >= burstCooldown)
            {
                isBursting = true;
                burstCount = 0;
                timer = 0f;
            }

            if (isBursting)
            {
                burstTimer += Time.deltaTime;
                if (burstTimer >= burstDelay)
                {
                    Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
                    ShootSMG(direction);
                    burstCount++;
                    burstTimer = 0f;

                    if (burstCount >= burstMax)
                    {
                        isBursting = false;
                        burstCount = 0;
                    }
                }
            }
        }
    }

    void ShootShotgun(Vector2 baseDirection)
    {
        float[] angles = doubleShot ?
            new float[] { -15f, -8f, 0f, 8f, 15f } :
            new float[] { -8f, 0f, 8f };

        foreach (float angle in angles)
        {
            Vector2 direction = RotateVector(baseDirection, angle);
            SpawnBullet(direction);
        }
    }

    void ShootSMG(Vector2 direction)
    {
        SpawnBullet(direction);
        if (doubleShot)
        {
            Vector2 dir2 = RotateVector(direction, 10f);
            SpawnBullet(dir2);
        }
    }

    void SpawnBullet(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
        Bullet b = bullet.GetComponent<Bullet>();
        b.damage = bulletDamage;
    }

    Vector2 RotateVector(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        return new Vector2(
            v.x * Mathf.Cos(rad) - v.y * Mathf.Sin(rad),
            v.x * Mathf.Sin(rad) + v.y * Mathf.Cos(rad)
        );
    }
}
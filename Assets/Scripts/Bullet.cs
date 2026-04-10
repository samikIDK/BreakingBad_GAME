using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    public float damage = 20f;
    public float lifetime = 3f;

    void Start()
    {
        // Bullet se automaticky zničí po 3 sekundách
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<HealthSystem>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
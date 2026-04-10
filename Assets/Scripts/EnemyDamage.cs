using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Damage")]
    public float damageAmount = 10f;
    public float damageCooldown = 1f;

    private float timer;

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            timer += Time.deltaTime;

            if (timer >= damageCooldown)
            {
                collision.gameObject.GetComponent<HealthSystem>().TakeDamage(damageAmount);
                timer = 0f;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        timer = 0f;
    }
}
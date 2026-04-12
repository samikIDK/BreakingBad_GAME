using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Damage")]
    public float damageAmount = 10f;
    public float damageCooldown = 1f;
    public float damageRadius = 0.8f;

    private float timer;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timer = damageCooldown;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= damageRadius)
        {
            timer += Time.deltaTime;

            if (timer >= damageCooldown)
            {
                player.GetComponent<HealthSystem>().TakeDamage(damageAmount);
                timer = 0f;
            }
        }
        else
        {
            timer = damageCooldown;
        }
    }
}
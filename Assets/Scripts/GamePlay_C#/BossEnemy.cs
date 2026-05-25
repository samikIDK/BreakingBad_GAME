using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 1.5f;
    public float dashSpeed = 8f;
    public float dashCooldown = 3f;
    public float spawnMinionCooldown = 5f;
    public GameObject minionPrefab;

    private Transform player;
    private Rigidbody2D rb;
    private float dashTimer;
    private float minionTimer;
    private bool isDashing = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        dashTimer = dashCooldown;
        minionTimer = spawnMinionCooldown;
    }

    void Update()
    {
        dashTimer -= Time.deltaTime;
        minionTimer -= Time.deltaTime;

        if (dashTimer <= 0f && !isDashing)
        {
            StartCoroutine(Dash());
            dashTimer = dashCooldown;
        }

        if (minionTimer <= 0f)
        {
            SpawnMinions();
            minionTimer = spawnMinionCooldown;
        }
    }

    void FixedUpdate()
    {
        if (!isDashing && player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;
        }
    }

    System.Collections.IEnumerator Dash()
    {
        isDashing = true;
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * dashSpeed;
        yield return new WaitForSeconds(0.3f);
        isDashing = false;
    }

    void SpawnMinions()
    {
        if (minionPrefab == null) return;

        for (int i = 0; i < 3; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 3f;
            Instantiate(minionPrefab, (Vector2)transform.position + randomOffset, Quaternion.identity);
        }
    }
}
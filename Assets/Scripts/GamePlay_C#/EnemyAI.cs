using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 2f;
    public float detectionRange = 9999f;

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

            if (animator != null)
            {
                animator.SetFloat("MoveX", direction.x);
                animator.SetFloat("MoveY", direction.y);
                animator.SetBool("IsMoving", true);
            }
        }
        else
        {
            if (animator != null)
                animator.SetBool("IsMoving", false);
        }
    }
}
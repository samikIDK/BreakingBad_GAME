using UnityEngine;
using TMPro;

public class PlayerDash : MonoBehaviour
{
    [Header("Dash Settings")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 5f;

    [Header("UI")]
    public TextMeshProUGUI dashCooldownText;

    private Rigidbody2D rb;
    private HealthSystem health;
    private float cooldownTimer = 0f;
    private bool isDashing = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<HealthSystem>();
    }

    void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            if (dashCooldownText != null)
                dashCooldownText.text = "DASH: " + cooldownTimer.ToString("F1") + "s";
        }
        else
        {
            if (dashCooldownText != null)
                dashCooldownText.text = "DASH: Ready ✅";
        }

        if (Time.timeScale == 0f) return;

        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer <= 0f && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    System.Collections.IEnumerator Dash()
    {
        isDashing = true;
        health.isInvincible = true;
        cooldownTimer = dashCooldown;

        PlayerMovement movement = GetComponent<PlayerMovement>();
        movement.enabled = false;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 dashDirection = new Vector2(x, y).normalized;

        if (dashDirection == Vector2.zero)
            dashDirection = Vector2.right;

        rb.linearVelocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        health.isInvincible = false;
        rb.linearVelocity = Vector2.zero;
        movement.enabled = true;
    }
}
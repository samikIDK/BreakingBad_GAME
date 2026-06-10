using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;
    public bool isInvincible = false;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        Debug.Log(gameObject.name + " HP: " + currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    void Die()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                player.GetComponent<XPSystem>().AddXP(20f);

            if (gameObject.name.Contains("Boss"))
                FindAnyObjectByType<GameOverUI>().ShowVictory();

            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
                GameManager.Instance.ResetIngameUpgrades();
            FindAnyObjectByType<GameOverUI>().ShowGameOver();
        }
    }

    public float GetCurrentHealth() { return currentHealth; }
    public float GetMaxHealth() { return maxHealth; }

    public IEnumerator Regen(float amount)
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        }
    }
}
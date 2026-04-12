using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " má HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.GetComponent<XPSystem>().AddXP(20f);
            }
            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("Player"))
        {
            GameObject.FindObjectOfType<GameOverUI>().ShowGameOver();
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public IEnumerator Regen(float amount)
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("HP Bar")]
    public Slider hpBar;
    public HealthSystem playerHealth;

    void Awake()
    {
        hpBar.minValue = 0;
        hpBar.maxValue = playerHealth.maxHealth;
        hpBar.value = playerHealth.maxHealth;
    }

    void Update()
    {
        hpBar.value = playerHealth.GetCurrentHealth();
    }
}
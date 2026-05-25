using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    public Slider bossHPBar;
    public HealthSystem bossHealth;

    void Start()
    {
        bossHPBar.minValue = 0;
        bossHPBar.maxValue = bossHealth.maxHealth;
        bossHPBar.value = bossHealth.maxHealth;
    }

    void Update()
    {
        if (bossHealth != null)
            bossHPBar.value = bossHealth.GetCurrentHealth();
    }
}
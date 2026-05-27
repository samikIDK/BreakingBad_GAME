using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Ingame upgrady (resetují se při Game Over)
    public float damageMultiplier = 1f;
    public float attackSpeedMultiplier = 1f;
    public float moveSpeedMultiplier = 1f;
    public float maxHPBonus = 0f;
    public float xpMultiplier = 1f;
    public float shootRangeMultiplier = 1f;
    public bool hasRegen = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetIngameUpgrades()
    {
        damageMultiplier = 1f;
        attackSpeedMultiplier = 1f;
        moveSpeedMultiplier = 1f;
        maxHPBonus = 0f;
        xpMultiplier = 1f;
        shootRangeMultiplier = 1f;
        hasRegen = false;
    }
}
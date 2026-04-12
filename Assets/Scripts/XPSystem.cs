using UnityEngine;

public class XPSystem : MonoBehaviour
{
    [Header("XP Settings")]
    public float currentXP = 0f;
    public float xpToNextLevel = 100f;
    public float xpMultiplier = 1.2f;
    public int currentLevel = 1;

    public delegate void LevelUpAction(int level);
    public event LevelUpAction OnLevelUp;

    public void AddXP(float amount)
    {
        currentXP += amount;

        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        currentXP -= xpToNextLevel;
        xpToNextLevel *= xpMultiplier;
        currentLevel++;

        Debug.Log("Level Up! Level: " + currentLevel);
        OnLevelUp?.Invoke(currentLevel);
    }

    public float GetXPProgress()
    {
        return currentXP / xpToNextLevel;
    }
}
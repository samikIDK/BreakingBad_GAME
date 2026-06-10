using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpUI : MonoBehaviour
{
    [Header("Panel")]
    public GameObject levelUpPanel;
    public Button[] upgradeCards;
    public TextMeshProUGUI[] upgradeTexts;
    
    [Header("Shield")]
    public GameObject shieldEffect;

    private XPSystem xpSystem;
    private GameObject player;
    private PlayerShooting shooting;
    private PlayerMovement movement;
    private HealthSystem health;

    private string[] allUpgrades = {
    "Damage +25%",
    "Attack Speed +20%",
    "Move Speed +15%",
    "Max HP +30",
    "HP Regen +2/s",
    "Shield",
    "Double Shot",
    "Heal +30 HP",
    "Bullet Speed +30%",
    "Dash (Space)",
    };

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        xpSystem = player.GetComponent<XPSystem>();
        shooting = player.GetComponent<PlayerShooting>();
        movement = player.GetComponent<PlayerMovement>();
        health = player.GetComponent<HealthSystem>();

        xpSystem.OnLevelUp += ShowLevelUpUI;
        levelUpPanel.SetActive(false);
    }

    void ShowLevelUpUI(int level)
    {
        levelUpPanel.SetActive(true);
        Time.timeScale = 0f;

        int[] chosen = GetRandomUpgrades(3);

        for (int i = 0; i < upgradeCards.Length; i++)
        {
            string upgrade = allUpgrades[chosen[i]];
            upgradeTexts[i].text = upgrade;

            int index = chosen[i];
            upgradeCards[i].onClick.RemoveAllListeners();
            upgradeCards[i].onClick.AddListener(() =>
            {
                SelectUpgrade(allUpgrades[index]);
            });
        }
    }

    int[] GetRandomUpgrades(int count)
    {
        int[] result = new int[count];
        System.Collections.Generic.List<int> available = new System.Collections.Generic.List<int>();
    
        for (int i = 0; i < allUpgrades.Length; i++)
        {
            // Přeskoč Double Shot pokud ho hráč už má
            if (allUpgrades[i] == "Double Shot" && shooting.doubleShot) continue;
            available.Add(i);
        }
    
        for (int i = 0; i < count; i++)
        {
            int rand = Random.Range(0, available.Count);
            result[i] = available[rand];
            available.RemoveAt(rand);
        }
        return result;
    }

    void SelectUpgrade(string upgrade)
    {
        Debug.Log("Selected: " + upgrade);

        switch (upgrade)
        {
            case "Damage +25%":
                shooting.bulletDamage *= 1.25f;
                if (GameManager.Instance != null)
                    GameManager.Instance.damageMultiplier *= 1.25f;
                break;
            case "Attack Speed +20%":
                shooting.fireRate *= 1.2f;
                if (GameManager.Instance != null)
                    GameManager.Instance.attackSpeedMultiplier *= 1.2f;
                break;
            case "Move Speed +15%":
                movement.moveSpeed *= 1.15f;
                if (GameManager.Instance != null)
                    GameManager.Instance.moveSpeedMultiplier *= 1.15f;
                break;
            case "Max HP +30":
                health.maxHealth += 30f;
                if (GameManager.Instance != null)
                    GameManager.Instance.maxHPBonus += 30f;
                break;
            case "HP Regen +2/s":
                health.StartCoroutine(health.Regen(2f));
                if (GameManager.Instance != null)
                    GameManager.Instance.hasRegen = true;
                break;
            case "Shield":
                shooting.StartCoroutine(ActivateShield());
                break;
            case "Double Shot":
                shooting.doubleShot = true;
                if (GameManager.Instance != null)
                    GameManager.Instance.doubleShot = true;
                break;
            case "Heal +30 HP":
                health.Heal(30f);
                break;
            case "Bullet Speed +30%":
                shooting.bulletSpeed *= 1.3f;
                if (GameManager.Instance != null)
                    GameManager.Instance.bulletSpeedMultiplier *= 1.3f;
                break;
            case "Dash (Space)":
                PlayerDash dash = player.GetComponent<PlayerDash>();
                if (dash != null)
                {
                    dash.enabled = true;
                    // Aktivuj UI text
                    GameObject dashText = GameObject.Find("DashCooldownText");
                    if (dashText != null) dashText.SetActive(true);
                }
                if (GameManager.Instance != null)
                    GameManager.Instance.hasDash = true;
                break;
        }

        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }

System.Collections.IEnumerator ActivateShield()
{
    health.isInvincible = true;
    if (shieldEffect != null) shieldEffect.SetActive(true);
    
    yield return new WaitForSecondsRealtime(3f);
    
    health.isInvincible = false;
    if (shieldEffect != null) shieldEffect.SetActive(false);
}
}
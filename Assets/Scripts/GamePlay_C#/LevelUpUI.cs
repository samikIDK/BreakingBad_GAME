using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpUI : MonoBehaviour
{
    [Header("Panel")]
    public GameObject levelUpPanel;
    public Button[] upgradeCards;
    public TextMeshProUGUI[] upgradeTexts;

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
        "XP Gain +25%",
        "Shoot Range +20%",
        "HP Regen +2/s"
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
            Debug.Log("Kliknuto! Upgrade: " + allUpgrades[index]);
            SelectUpgrade(allUpgrades[index]);
        });
    }
}

    int[] GetRandomUpgrades(int count)
    {
        int[] result = new int[count];
        System.Collections.Generic.List<int> available = new System.Collections.Generic.List<int>();

        for (int i = 0; i < allUpgrades.Length; i++)
            available.Add(i);

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
        switch (upgrade)
        {
            case "Damage +25%":
                shooting.bulletDamage *= 1.25f;
                break;
            case "Attack Speed +20%":
                shooting.fireRate *= 1.2f;
                break;
            case "Move Speed +15%":
                movement.moveSpeed *= 1.15f;
                break;
            case "Max HP +30":
                health.maxHealth += 30f;
                break;
            case "XP Gain +25%":
                xpSystem.xpToNextLevel *= 0.75f;
                break;
            case "Shoot Range +20%":
                shooting.shootRange *= 1.2f;
                break;
            case "HP Regen +2/s":
                health.StartCoroutine(health.Regen(2f));
                break;
        }

        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
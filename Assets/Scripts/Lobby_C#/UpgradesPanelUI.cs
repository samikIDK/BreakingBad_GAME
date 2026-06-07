using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradesPanelUI : MonoBehaviour
{
    [Header("Walter")]
    public TextMeshProUGUI walterLevelText;
    public TextMeshProUGUI walterStatsText;
    public Button walterUpgradeButton;
    public TextMeshProUGUI walterUpgradeCostText;

    [Header("Jesse")]
    public TextMeshProUGUI jesseLevelText;
    public TextMeshProUGUI jesseStatsText;
    public Button jesseUpgradeButton;
    public TextMeshProUGUI jesseUpgradeCostText;

    private int upgradeCost = 50;
    private int maxLevel = 10;

    void OnEnable()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        int chemicals = PlayerPrefs.GetInt("Chemicals", 0);
        int walterLevel = PlayerPrefs.GetInt("WalterLevel", 1);
        int jesseLevel = PlayerPrefs.GetInt("JesseLevel", 1);

        if (walterLevelText != null)
            walterLevelText.text = "Level: " + walterLevel + "/" + maxLevel;
        if (walterStatsText != null)
            walterStatsText.text =
                "HP: " + (100 + (walterLevel - 1) * 10) + "\n" +
                "DMG: " + (40 + (walterLevel - 1) * 5) + "\n" +
                "Speed: " + (5 + (walterLevel - 1) * 0.25f).ToString("F1");
        if (walterUpgradeCostText != null)
            walterUpgradeCostText.text = walterLevel >= maxLevel ? "MAX" : "UPGRADE\n " + upgradeCost;
        if (walterUpgradeButton != null)
            walterUpgradeButton.interactable = walterLevel < maxLevel && chemicals >= upgradeCost;

        if (jesseLevelText != null)
            jesseLevelText.text = "Level: " + jesseLevel + "/" + maxLevel;
        if (jesseStatsText != null)
            jesseStatsText.text =
                "HP: " + (100 + (jesseLevel - 1) * 10) + "\n" +
                "DMG: " + (15 + (jesseLevel - 1) * 3) + "\n" +
                "Speed: " + (5 + (jesseLevel - 1) * 0.35f).ToString("F1");
        if (jesseUpgradeCostText != null)
            jesseUpgradeCostText.text = jesseLevel >= maxLevel ? "MAX" : "UPGRADE\n " + upgradeCost;
        if (jesseUpgradeButton != null)
            jesseUpgradeButton.interactable = jesseLevel < maxLevel && chemicals >= upgradeCost;
    }

    public void UpgradeWalter()
    {
        Upgrade("Walter");
    }

    public void UpgradeJesse()
    {
        Upgrade("Jesse");
    }

    void Upgrade(string character)
    {
        int chemicals = PlayerPrefs.GetInt("Chemicals", 0);
        int level = PlayerPrefs.GetInt(character + "Level", 1);

        if (chemicals < upgradeCost || level >= maxLevel) return;

        level++;
        chemicals -= upgradeCost;
        PlayerPrefs.SetInt(character + "Level", level);
        PlayerPrefs.SetInt("Chemicals", chemicals);
        PlayerPrefs.Save();

        UpdateUI();
    }
}
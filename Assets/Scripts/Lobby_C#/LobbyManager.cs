using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI currencyText;

    [Header("Characters")]
    public Button walterButton;
    public Button jesseButton;

    [Header("Walter Upgrade")]
    public TextMeshProUGUI walterLevelText;
    public Button walterUpgradeButton;

    [Header("Jesse Upgrade")]
    public TextMeshProUGUI jesseLevelText;
    public Button jesseUpgradeButton;

    private int chemicals;
    private int walterLevel;
    private int jesseLevel;
    private int maxLevel = 10;
    private int upgradeCost = 50;
    private string selectedCharacter = "Walter";

    void Start()
    {
        chemicals = PlayerPrefs.GetInt("Chemicals", 0);
        walterLevel = PlayerPrefs.GetInt("WalterLevel", 1);
        jesseLevel = PlayerPrefs.GetInt("JesseLevel", 1);

        walterButton.onClick.AddListener(() => SelectCharacter("Walter"));
        jesseButton.onClick.AddListener(() => SelectCharacter("Jesse"));
        walterUpgradeButton.onClick.AddListener(() => UpgradeCharacter("Walter"));
        jesseUpgradeButton.onClick.AddListener(() => UpgradeCharacter("Jesse"));

        UpdateUI();
    }

    void SelectCharacter(string character)
    {
        selectedCharacter = character;
        PlayerPrefs.SetString("SelectedCharacter", character);
        Debug.Log("Selected: " + character);
        UpdateUI();
    }

    void UpgradeCharacter(string character)
    {
        if (chemicals < upgradeCost) 
        {
            Debug.Log("Not enough chemicals!");
            return;
        }

        if (character == "Walter" && walterLevel < maxLevel)
        {
            walterLevel++;
            PlayerPrefs.SetInt("WalterLevel", walterLevel);
            chemicals -= upgradeCost;
            PlayerPrefs.SetInt("Chemicals", chemicals);
            PlayerPrefs.Save();
            Debug.Log("Walter upgraded to level: " + walterLevel);
        }
        else if (character == "Jesse" && jesseLevel < maxLevel)
        {
            jesseLevel++;
            PlayerPrefs.SetInt("JesseLevel", jesseLevel);
            chemicals -= upgradeCost;
            PlayerPrefs.SetInt("Chemicals", chemicals);
            PlayerPrefs.Save();
            Debug.Log("Jesse upgraded to level: " + jesseLevel);
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        currencyText.text = "⚗️ Chemicals: " + chemicals;
        walterLevelText.text = "Level: " + walterLevel + "/" + maxLevel;
        jesseLevelText.text = "Level: " + jesseLevel + "/" + maxLevel;

        walterUpgradeButton.interactable = walterLevel < maxLevel && chemicals >= upgradeCost;
        jesseUpgradeButton.interactable = jesseLevel < maxLevel && chemicals >= upgradeCost;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
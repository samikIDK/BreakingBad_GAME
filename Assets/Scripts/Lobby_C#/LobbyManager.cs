using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject characterSelectPanel;
    public GameObject upgradesPanel;
    public GameObject settingsPanel;

    [Header("Currency")]
    public TextMeshProUGUI chemicalsText;

    [Header("Settings")]
    public Slider volumeSlider;

    void Start()
    {
        UpdateChemicals();
        ShowMainMenu();

        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    void UpdateChemicals()
    {
        int chemicals = PlayerPrefs.GetInt("Chemicals", 0);
        if (chemicalsText != null)
            chemicalsText.text = "⚗️ " + chemicals;
    }

    // Main Menu
    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        characterSelectPanel.SetActive(false);
        upgradesPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
    }

    // Play → Character Select
    public void OnPlayButton()
    {
        mainMenuPanel.SetActive(false);
        characterSelectPanel.SetActive(true);
    }

    // Character Select
    public void SelectWalter()
    {
        PlayerPrefs.SetString("SelectedCharacter", "Walter");
        PlayerPrefs.Save();
        SceneManager.LoadScene("GamePlay");
    }

    public void SelectJesse()
    {
        PlayerPrefs.SetString("SelectedCharacter", "Jesse");
        PlayerPrefs.Save();
        SceneManager.LoadScene("GamePlay");
    }

    // Upgrades
    public void OnUpgradesButton()
    {
        mainMenuPanel.SetActive(false);
        upgradesPanel.SetActive(true);
    }

    public void OnUpgradeWalter()
    {
        UpgradeCharacter("Walter");
    }

    public void OnUpgradeJesse()
    {
        UpgradeCharacter("Jesse");
    }

    void UpgradeCharacter(string character)
    {
        int chemicals = PlayerPrefs.GetInt("Chemicals", 0);
        int level = PlayerPrefs.GetInt(character + "Level", 1);
        int cost = 50;
        int maxLevel = 10;

        if (chemicals < cost)
        {
            Debug.Log("Not enough chemicals!");
            return;
        }

        if (level >= maxLevel)
        {
            Debug.Log("Max level reached!");
            return;
        }

        level++;
        chemicals -= cost;
        PlayerPrefs.SetInt(character + "Level", level);
        PlayerPrefs.SetInt("Chemicals", chemicals);
        PlayerPrefs.Save();
        UpdateChemicals();
        Debug.Log(character + " upgraded to level: " + level);
    }

    // Settings
    public void OnSettingsButton()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    void SetVolume(float value)
    {
        AudioListener.volume = value;
    }

    // Quit
    public void OnQuitButton()
    {
        Application.Quit();
    }

    // Back buttons
    public void BackToMainMenu()
    {
        ShowMainMenu();
        UpdateChemicals();
    }
}
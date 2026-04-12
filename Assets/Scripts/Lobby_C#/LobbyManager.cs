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

    private int chemicals = 0;
    private string selectedCharacter = "Walter";

    void Start()
    {
        // Načti chemikálie z předchozí hry
        chemicals = PlayerPrefs.GetInt("Chemicals", 0);
        UpdateUI();

        walterButton.onClick.AddListener(() => SelectCharacter("Walter"));
        jesseButton.onClick.AddListener(() => SelectCharacter("Jesse"));
    }

    void SelectCharacter(string character)
    {
        selectedCharacter = character;
        PlayerPrefs.SetString("SelectedCharacter", character);
        Debug.Log("Selected: " + character);
        UpdateUI();
    }

    void UpdateUI()
    {
        currencyText.text = "⚗️ Chemicals: " + chemicals;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
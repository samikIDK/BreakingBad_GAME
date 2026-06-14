using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Panel")]
    public GameObject pausePanel;

    [Header("UI")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI upgradesText;

    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);
    }


void Update()
{
    if (!isPaused && GameManager.Instance != null)
        GameManager.Instance.survivalTime += Time.deltaTime;

    if (Input.GetKeyDown(KeyCode.Escape))
    {
        if (isPaused) Resume();
        else Pause();
    }
}

void Pause()
{
    isPaused = true;
    pausePanel.SetActive(true);
    Time.timeScale = 0f;

    if (AudioManager.Instance != null)
        AudioManager.Instance.musicSource.Pause();

    float survivalTime = GameManager.Instance != null ? GameManager.Instance.survivalTime : 0f;
    int minutes = Mathf.FloorToInt(survivalTime / 60f);
    int seconds = Mathf.FloorToInt(survivalTime % 60f);
    timeText.text = "Survival Time: " + minutes + ":" + seconds.ToString("00");

    UpdateUpgradesText();
}

    public void Resume()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        if (AudioManager.Instance != null)
            AudioManager.Instance.musicSource.UnPause();
    }

    void UpdateUpgradesText()
{
    if (GameManager.Instance == null)
    {
        upgradesText.text = "None yet!";
        return;
    }

    string text = "";
    bool hasAny = false;

    if (GameManager.Instance.damageMultiplier > 1f)
    {
        text += "• Damage: x" + GameManager.Instance.damageMultiplier.ToString("F2") + "\n";
        hasAny = true;
    }
    if (GameManager.Instance.attackSpeedMultiplier > 1f)
    {
        text += "• Attack Speed: x" + GameManager.Instance.attackSpeedMultiplier.ToString("F2") + "\n";
        hasAny = true;
    }
    if (GameManager.Instance.moveSpeedMultiplier > 1f)
    {
        text += "• Move Speed: x" + GameManager.Instance.moveSpeedMultiplier.ToString("F2") + "\n";
        hasAny = true;
    }
    if (GameManager.Instance.maxHPBonus > 0f)
    {
        text += "• Max HP: +" + GameManager.Instance.maxHPBonus + "\n";
        hasAny = true;
    }
    if (GameManager.Instance.hasRegen)
    {
        text += "• HP Regen: +2/s\n";
        hasAny = true;
    }
    if (GameManager.Instance.doubleShot)
    {
        text += "• Double Shot \n";
        hasAny = true;
    }
    if (GameManager.Instance.bulletSpeedMultiplier > 1f)
    {
        text += "• Bullet Speed: x" + GameManager.Instance.bulletSpeedMultiplier.ToString("F2") + "\n";
        hasAny = true;
    }

    if (!hasAny)
        text = "None yet!";

    upgradesText.text = text;
}

    public void BackToLobby()
    {
        Time.timeScale = 1f;

        float survivalTime = GameManager.Instance != null ? GameManager.Instance.survivalTime : 0f;
        int earnedChemicals = Mathf.FloorToInt(survivalTime / 5f);
        int currentChemicals = PlayerPrefs.GetInt("Chemicals", 0);
        PlayerPrefs.SetInt("Chemicals", currentChemicals + earnedChemicals);
        PlayerPrefs.Save();

        if (GameManager.Instance != null)
            GameManager.Instance.ResetIngameUpgrades();

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMusic(AudioManager.Instance.lobbyMusic);

        SceneManager.LoadScene("Lobby");
    }
}
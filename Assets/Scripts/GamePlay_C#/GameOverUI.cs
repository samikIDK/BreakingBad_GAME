using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [Header("Panel")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI timeText;

    private float survivalTime;
    private bool isGameOver = false;

    void Update()
    {
        if (!isGameOver)
        {
            survivalTime += Time.deltaTime;
        }
    }

    public void ShowGameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

        int minutes = Mathf.FloorToInt(survivalTime / 60f);
        int seconds = Mathf.FloorToInt(survivalTime % 60f);
        timeText.text = "Survival Time: " + minutes + ":" + seconds.ToString("00");

        int earnedChemicals = CalculateChemicals();
        int currentChemicals = PlayerPrefs.GetInt("Chemicals", 0);
        PlayerPrefs.SetInt("Chemicals", currentChemicals + earnedChemicals);
        PlayerPrefs.Save();

        Debug.Log("Earned chemicals: " + earnedChemicals);
    }

    int CalculateChemicals()
    {
        return Mathf.FloorToInt(survivalTime / 5f);
    }

    public void BackToLobby()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Lobby");
    }
}
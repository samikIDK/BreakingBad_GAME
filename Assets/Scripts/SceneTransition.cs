using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneTransition : MonoBehaviour
{
    [Header("Settings")]
    public float mapDuration = 420f; // 7 minut = 420 sekund
    public string nextScene = "Lab";

    [Header("UI")]
    public TextMeshProUGUI timerText;

    private float timer;
    private bool transitioning = false;

    void Update()
    {
        if (transitioning) return;

        timer += Time.deltaTime;

        // Zobraz zbývající čas
        float remaining = mapDuration - timer;
        int minutes = Mathf.FloorToInt(remaining / 60f);
        int seconds = Mathf.FloorToInt(remaining % 60f);
        if (timerText != null)
            timerText.text = minutes + ":" + seconds.ToString("00");

        if (timer >= mapDuration)
        {
            transitioning = true;
            SceneManager.LoadScene(nextScene);
        }
    }
}
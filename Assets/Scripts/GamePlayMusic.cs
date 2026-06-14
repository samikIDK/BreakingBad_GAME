using UnityEngine;

public class GamePlayMusic : MonoBehaviour
{
    void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.musicSource.Stop();
            AudioManager.Instance.PlayMusic(AudioManager.Instance.gameplayMusic);
        }
    }
}
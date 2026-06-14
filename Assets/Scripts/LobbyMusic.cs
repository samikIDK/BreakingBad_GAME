using UnityEngine;

public class LobbyMusic : MonoBehaviour
{
    void Start()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMusic(AudioManager.Instance.lobbyMusic);
    }
}
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    public AudioSource musicSource;

    [Header("SFX")]
    public AudioSource sfxSource;

    [Header("Clips - Extra")]
    public AudioClip buttonClick;

    [Header("Clips - Music")]
    public AudioClip lobbyMusic;
    public AudioClip gameplayMusic;

    [Header("Clips - SFX")]
    public AudioClip walterShoot;
    public AudioClip jesseShoot;
    public AudioClip enemyHit;
    public AudioClip levelUp;
    public AudioClip dashSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.volume = 0.2f; // Tišší hudba
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume = 1.0f)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip, volume);
    }

    public void PlayWalterShoot() => PlaySFX(walterShoot, 0.2f); 
    public void PlayJesseShoot() => PlaySFX(jesseShoot, 0.3f);  
    public void PlayEnemyHit() => PlaySFX(enemyHit, 2.0f);      
    public void PlayLevelUp() => PlaySFX(levelUp, 1.0f);
    public void PlayDash() => PlaySFX(dashSound, 0.8f);
    public void PlayButtonClick() => PlaySFX(buttonClick, 0.5f);
}
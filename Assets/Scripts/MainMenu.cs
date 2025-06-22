using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip menuMusicClip;
    public AudioClip buttonClickClip;

    void Start()
    {
        // Play background music once on start
        if (musicSource != null && menuMusicClip != null)
        {
            musicSource.clip = menuMusicClip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayGame()
    {
        PlayButtonSound();
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        PlayButtonSound();
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void PlayButtonSound()
    {
        if (sfxSource != null && buttonClickClip != null)
        {
            sfxSource.PlayOneShot(buttonClickClip);
        }
    }
}

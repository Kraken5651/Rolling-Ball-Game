using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("UI")]
    public TextMeshProUGUI countText;
    public GameObject winMenuPanel;
    public GameObject loseMenuPanel;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusicClip;
    public AudioClip pickupClip;
    public AudioClip enemyHitClip;
    public AudioClip buttonClickClip;

    private int pickupCount = 0;
    private int totalPickups;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // UI Setup
        if (winMenuPanel) winMenuPanel.SetActive(false);
        if (loseMenuPanel) loseMenuPanel.SetActive(false);

        // Audio Setup
        if (musicSource && backgroundMusicClip)
        {
            musicSource.clip = backgroundMusicClip;
            musicSource.loop = true;
            musicSource.Play();
        }

        // Pickup Count
        totalPickups = GameObject.FindGameObjectsWithTag("PickUp").Length;
        UpdateUI();
    }

    public void CollectPickup()
    {
        pickupCount++;
        Debug.Log("Pickup collected: " + pickupCount + "/" + totalPickups);

        if (sfxSource && pickupClip)
        {
            sfxSource.PlayOneShot(pickupClip);
        }

        UpdateUI();

        if (pickupCount >= totalPickups)
        {
            WinLevel();
        }
    }

    public void PlayerLost()
    {
        Time.timeScale = 0f;

        if (sfxSource && enemyHitClip)
        {
            sfxSource.PlayOneShot(enemyHitClip);
        }

        if (loseMenuPanel)
        {
            loseMenuPanel.SetActive(true);
        }
    }

    private void WinLevel()
    {
        Time.timeScale = 0f;

        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy) Destroy(enemy);

        if (winMenuPanel)
        {
            winMenuPanel.SetActive(true);
        }
    }

    private void UpdateUI()
    {
    if (countText != null)
    {
        countText.text = "Collected: " + pickupCount + "/" + totalPickups;
    }

    }

    // ====== UI Button Methods ======

    public void LoadNextLevel()
    {
        PlayButtonSound();
        Time.timeScale = 1f;

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No next level found. Returning to Main Menu.");
            SceneManager.LoadScene(0);
        }
    }

    public void RestartLevel()
    {
        PlayButtonSound();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        if (sfxSource && buttonClickClip)
        {
            sfxSource.PlayOneShot(buttonClickClip);
        }
    }
}

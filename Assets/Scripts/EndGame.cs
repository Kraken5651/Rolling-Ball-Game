using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] GameObject endGameUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (endGameUI != null)
        {
            endGameUI.SetActive(false);
        }
        else
        {
            Debug.LogError("EndGameUI is not assigned in the Inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the game is over (this could be based on a condition in your game logic)
        
    }

    public void NextLevel()
    {

    }

    public void Restart()
    {
        // Reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();

        // If running in the editor, stop playing the scene
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}

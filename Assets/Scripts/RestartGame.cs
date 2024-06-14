using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour
{
    public Button restartButton; // Reference to the Restart Button

    private void Start()
    {
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(Restart); // Add listener to the Restart button
        }
    }

    private void Restart()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

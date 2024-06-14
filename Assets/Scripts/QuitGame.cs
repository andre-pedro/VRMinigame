using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    public Button quitButton; // Reference to the Quit Button

    private void Start()
    {
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(Quit); // Add listener to the Quit button
        }
    }

    private void Quit()
    {
        // Quit the application
        Debug.Log("Quitting game...");
        Application.Quit();

#if UNITY_EDITOR
        // If running in the Unity Editor, stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

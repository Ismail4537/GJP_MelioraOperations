using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Credit : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            MainMenuBtn();
        }
    }
    public void MainMenuBtn()
    {
        // Load the main menu scene (assuming it's named "MainMenu")
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1; // Ensure time scale is reset when loading a new scene
    }
}

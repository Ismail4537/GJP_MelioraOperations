using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PauseMenuController : MonoBehaviour
{
    public GameObject container;
    public static PauseMenuController instance;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //setSliderValues();
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            // Toggle pause menu
            if (container.activeSelf)
            {
                // Jika pause menu sedang aktif, tutup pause menu
                ResumeGame();
            }
            else
            {
                // Jika pause menu tidak aktif, buka pause menu
                Pause();
            }
        }
    }

    public void Pause()
    {
        container.SetActive(true);
        //setSliderValues();
        Time.timeScale = 0; // Pause the game
    }

    public void ResumeGame()
    {
        container.SetActive(false);
        Time.timeScale = 1; // Resume the game
    }

    public void MainMenuBtn()
    {
        SceneController.instance.ToMainMenu();
        Time.timeScale = 1; // Ensure time scale is reset when loading a new scene
    }

    public void RestartGame()
    {
        container.SetActive(false);
        Time.timeScale = 1; // Ensure time scale is reset when restarting the game
        SceneController.instance.RestartScene();
    }

    //void setSliderValues()
    //{
    //    masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
    //    musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
    //    sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
    //}
}
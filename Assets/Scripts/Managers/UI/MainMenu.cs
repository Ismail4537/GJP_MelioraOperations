using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenuButtonsContainer;
    //[SerializeField] Slider masterSlider;
    //[SerializeField] Slider musicSlider;
    //[SerializeField] Slider sfxSlider;
    private void Start()
    {
        MusicManager.instance.PlayMusicTrack("MainMenu", 1f);
        //setSliderValues();
    }

    public void LoadScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
        Time.timeScale = 1;
    }

    public void ToggleContainer(GameObject container)
    {
        bool isActive = container.activeSelf;
        container.SetActive(!isActive);
        ToggleInteractableButtons(mainMenuButtonsContainer, isActive);
        //setSliderValues();
    }

    void ToggleInteractableButtons(GameObject container, bool isInteractable)
    {
        Button[] buttons = container.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            button.interactable = isInteractable;
        }
    }

    //void setSliderValues()
    //{
    //    masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
    //    musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
    //    sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
    //}

    public void ExitGame()
    {
        Debug.Log("Exitting game...");
        Application.Quit();
    }
}

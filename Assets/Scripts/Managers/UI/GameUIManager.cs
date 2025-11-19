using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager instance;
    [SerializeField] GameObject HUD;
    [SerializeField] GameObject gameOverScreen;
    // [SerializeField] Button nextStageButton;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        toggleHUD(true);
    }

    public void toggleHUD(bool isActive)
    {
        HUD.SetActive(isActive);
    }

    public void ShowGameOverScreen()
    {
        toggleHUD(false);

        // if (isWin)
        // {
        //     nextStageButton.gameObject.SetActive(true);
        // }
        // else
        // {
        //     nextStageButton.gameObject.SetActive(false);
        // }

        gameOverScreen.SetActive(true);
    }

    public bool isGameOverScreenActive()
    {
        return gameOverScreen.activeSelf;
    }

    public void RestartBtn()
    {
        gameOverScreen.SetActive(false);
        SceneController.instance.RestartScene();
        toggleHUD(true);
    }

    public void MainMenuBtn()
    {
        gameOverScreen.SetActive(false);
        SceneController.instance.ToMainMenu();
    }

    // public void NextStageBtn()
    // {
    //     gameOverScreen.SetActive(false);
    //     SceneController.instance.ToNextScene();
    // }
}

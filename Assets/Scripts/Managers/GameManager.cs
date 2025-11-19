using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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

    }

    public void triggerGameOver()
    {
        if (GameUIManager.instance.isGameOverScreenActive())
            return;
        MusicManager.instance.StopMusicTrack(0.8f);
        saveCurrStageData();
        SFXManager.instance.PlayClip2D("GameOver", 1.0f);
        GameUIManager.instance.ShowGameOverScreen();
    }

    public void triggerWin(string title = "Score")
    {
        if (GameUIManager.instance.isGameOverScreenActive())
            return;
        saveCurrStageData();
        SFXManager.instance.PlayClip2D("Win", 1.0f);
        // PlayerPrefs.SetString("Stage_" + SceneManager.GetActiveScene().buildIndex + "_Win", "Yes");
        GameUIManager.instance.ShowGameOverScreen();
    }

    public void resetGameData()
    {

    }

    public void saveCurrStageData()
    {
        string stageKey = "Stage_" + SceneManager.GetActiveScene().buildIndex;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    int maxScene;

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
        maxScene = SceneManager.sceneCountInBuildSettings - 1;
    }

    public void LoadScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
        Time.timeScale = 1;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.instance.saveCurrStageData();
        GameManager.instance.resetGameData();
        Time.timeScale = 1;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
        GameManager.instance.resetGameData();
        Destroy(GameUIManager.instance.gameObject);
    }

    public void ToGameScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
        Time.timeScale = 1;
        GameManager.instance.resetGameData();
    }

    public void ToNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        Debug.Log("Next Scene Index: " + nextSceneIndex);

        if (nextSceneIndex <= maxScene && nextSceneIndex >= 0)
        {
            ToGameScene(nextSceneIndex);
        }
        else
        {
            ToMainMenu();
        }
    }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Singleton instance
    public static ScoreManager Instance { get; private set; }

    [Header("Score Settings")]
    [SerializeField] private int currentScore = 0;
    [SerializeField] private int scoreIncrement = 10;
    [SerializeField] private int scoreDecrement = 5;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private int highScore = 0;
    private const string HIGH_SCORE_KEY = "HighScore";

    void Awake()
    {
        // Setup Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadHighScore();
        UpdateScoreUI();
    }

    // Menambah score
    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreUI();
        CheckHighScore();
    }

    // Mengurangi score
    public void SubtractScore(int points)
    {
        currentScore -= points;

        // Pastikan score tidak negatif (optional)
        if (currentScore < 0)
        {
            currentScore = 0;
        }

        UpdateScoreUI();
    }

    // Menambah score dengan nilai default
    public void AddDefaultScore()
    {
        AddScore(scoreIncrement);
    }

    // Mengurangi score dengan nilai default
    public void SubtractDefaultScore()
    {
        SubtractScore(scoreDecrement);
    }

    //// Reset score ke 0
    //public void ResetScore()
    //{
    //    currentScore = 0;
    //    UpdateScoreUI();
    //}

    // Update tampilan UI
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }

        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }

    // Cek dan update high score
    private void CheckHighScore()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            SaveHighScore();
            UpdateScoreUI();
        }
    }

    // Simpan high score ke PlayerPrefs
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt(HIGH_SCORE_KEY, highScore);
        PlayerPrefs.Save();
    }

    // Load high score dari PlayerPrefs
    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
    }

    //// Reset high score (untuk testing)
    //public void ResetHighScore()
    //{
    //    highScore = 0;
    //    PlayerPrefs.SetInt(HIGH_SCORE_KEY, 0);
    //    PlayerPrefs.Save();
    //    UpdateScoreUI();
    //}

    // Getter untuk current score
    public int GetCurrentScore()
    {
        return currentScore;
    }

    // Getter untuk high score
    public int GetHighScore()
    {
        return highScore;
    }
}
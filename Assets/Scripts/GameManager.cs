using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    [Header("Audio")]
    public AudioClip fallSound;
    public AudioSource musicSource;
    private AudioSource audioSource;

    [Header("Ball Reference")]
    public BallController ball;

    private int score = 0;
    private int lives = 3;

    private bool isDoubleScoreActive = false;
    private float doubleScoreEndTime;

    private bool isSlowBallActive = false;
    private float slowBallEndTime;

    void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        UpdateScoreUI();
        UpdateLivesUI();
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
    }

    void Update()
    {
        if (isDoubleScoreActive && Time.time >= doubleScoreEndTime)
        {
            isDoubleScoreActive = false;
            Debug.Log("🎯 Double Score ended.");
        }

        if (isSlowBallActive && Time.time >= slowBallEndTime)
        {
            isSlowBallActive = false;
            if (ball != null)
                ball.ResetSpeed();
            Debug.Log("🐢 Slow Ball ended.");
        }
    }

    public void AddScore(int value)
    {
        int finalScore = isDoubleScoreActive ? value * 2 : value;
        score += finalScore;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void LoseLife()
    {
        if (lives <= 0) return;

        lives--;
        UpdateLivesUI();

        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void AddLife()
    {
        lives++;
        UpdateLivesUI();
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
            livesText.text = "Lives: " + lives;
    }

    public void GameOver()
    {
        if (musicSource != null && musicSource.isPlaying)
            musicSource.Stop();

        if (audioSource != null && fallSound != null)
            audioSource.PlayOneShot(fallSound);

        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BuyLifeButton()
    {
        IAPManager.instance?.BuyExtraLife();
    }

    // ✅ این متد حالا درست تعریف شده است
    public void ActivateSpecialItem(string effectType, float duration)
    {
        if (effectType == "DoubleScore")
        {
            isDoubleScoreActive = true;
            doubleScoreEndTime = Time.time + duration;
            Debug.Log("✅ Double Score Activated");
        }
        else if (effectType == "SpeedUp")
        {
            ball.SetFastSpeed(); // در BallController اضافه کن
            Invoke(nameof(ResetBallSpeed), duration);
            Debug.Log("⚡ Speed Up Activated");
        }
    }
    void ResetBallSpeed()
{
    ball.ResetSpeed();
    Debug.Log("⚡ Speed Reset to Normal");
}
}

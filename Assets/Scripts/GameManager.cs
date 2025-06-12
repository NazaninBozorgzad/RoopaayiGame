using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    public AudioClip fallSound;
    public AudioSource musicSource;
    private AudioSource audioSource;

    int score = 0;
    int lives = 3;  // تعداد جان اولیه

    void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        UpdateScoreUI();
        UpdateLivesUI();
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
    }

    // ⬆️ افزایش امتیاز
    public void AddScore(int value)
    {
        score += value;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    // ⬇️ از دست دادن جان
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

    // ➕ افزودن جان (مثلاً از خرید درون‌برنامه‌ای)
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

    // ☠️ پایان بازی
    public void GameOver()
    {
        if (musicSource != null && musicSource.isPlaying)
            musicSource.Stop();

        audioSource?.PlayOneShot(fallSound);

        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    // 🔄 ریست بازی
    public void ReloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 🛒 فراخوانی خرید جان (از دکمه)
    public void BuyLifeButton()
    {
        IAPManager.instance?.BuyExtraLife();
    }
}

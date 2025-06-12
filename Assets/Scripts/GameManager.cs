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

    public void LoseLife()
    {
        if (lives <= 0) return; // اگر جان تموم شده دیگه کاری نکن

        lives--;
        UpdateLivesUI();

        if (lives <= 0)
        {
            GameOver();
        }
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

        audioSource?.PlayOneShot(fallSound);

        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

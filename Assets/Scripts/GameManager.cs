using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    public AudioClip fallSound;

    public AudioSource musicSource; // ← اضافه شد: صدای موزیک جدا
    private AudioSource audioSource;

    int score = 0;

    void Awake()
    {
        instance = this;
        Debug.Log("✅ GameManager فعال شد");
        audioSource = GetComponent<AudioSource>();
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
        Debug.Log("✅ امتیاز اضافه شد: " + score);
    }

    public void GameOver()
    {
        // قطع موزیک پس‌زمینه اگر فعال باشه
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }

        // پخش صدای افتادن یا باخت
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

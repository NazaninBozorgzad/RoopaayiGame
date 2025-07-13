using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject beforeStartImage;

    public AudioClip fallSound;
    public AudioSource musicSource;
    private AudioSource audioSource;

    public bool lost;

    int score = 0;
    int lives = 3;  // تعداد جان اولیه

    [Header("CountDownSheets")]
    public GameObject[] countDownSprites = new GameObject[3];

    void Awake()
    {
        lives += PlayerPrefs.GetInt("Purchased Lives");
        Time.timeScale = 0;
        BeginPlay();
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

    void UpdateLivesUI()
    {
        if (livesText != null)
            livesText.text = "Lives: " + lives;
    }

    // ☠️ پایان بازی
    public void GameOver()
    {
        // if (musicSource != null && musicSource.isPlaying)
        //     musicSource.Stop();

        lost = true;

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

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        //Will be replaced soon
        SceneManager.LoadScene("Main Menu");
    }

    // 🛒 فراخوانی خرید جان (از دکمه)
    public void BuyLifeButton()
    {
        IAPManager.instance?.BuyExtraLife();
    }

    System.Collections.IEnumerator BeginCountDown()
    {
        // I will restructure these if I get extra time
        beforeStartImage.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        countDownSprites[0].SetActive(true);

        yield return new WaitForSecondsRealtime(1);

        countDownSprites[0].SetActive(false);
        countDownSprites[1].SetActive(true);

        yield return new WaitForSecondsRealtime(1);

        countDownSprites[1].SetActive(false);
        countDownSprites[2].SetActive(true);

        yield return new WaitForSecondsRealtime(1);

        countDownSprites[2].SetActive(false);
        beforeStartImage.SetActive(false);
        Time.timeScale = 1;

        yield return null;

    }

    void BeginPlay()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        UpdateScoreUI();
        musicSource = GameObject.Find("MusicPlayer").GetComponent<AudioSource>();
        UpdateLivesUI();
        gameOverPanel.SetActive(false);
        StartCoroutine(BeginCountDown());
    }
}

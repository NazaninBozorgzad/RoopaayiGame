using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading;
using Unity.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject winningPanel;
    private LevelLoader levelLoader;

    [Header("Audio")]
    public AudioClip fallSound;
    public AudioClip winningSound;
    public AudioSource musicSource;
    public enum Level
    {
        SoccerStadium,
        BasketBallStadium,
        TennisPlayground
    };
    public Level level;
    private AudioSource audioSource;

    public bool lost;
    public bool hasInfiniteLives;

    int score = 0;
    int lives = 3;  // تعداد جان اولیه
    [Header("Ball Reference")]
    public BallController ball;

    private bool isDoubleScoreActive = false;
    private float doubleScoreEndTime;

    private bool isSlowBallActive = false;
    private float slowBallEndTime;


    void Awake()
    {
        switch (level)
        {
            case Level.SoccerStadium:
                {
                    score = 0;
                    break;
                }

            case Level.BasketBallStadium:
                {
                    score = 20;
                    break;
                }

            case Level.TennisPlayground:
                {
                    score = 40;
                    break;
                }
        }

        lives += PlayerPrefs.GetInt("Purchased Lives");
        BeginPlay();
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
        CheckSceneTransition();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void LoseLife(bool isInfinite)
    {
        if (isInfinite == true) return;
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
        if (livesText != null && hasInfiniteLives == true)
            return;
        else if (livesText != null && hasInfiniteLives == false)
            livesText.text = "Lives: " + lives;
    }

    public void GameOver()
    {
        if (musicSource != null && musicSource.isPlaying)
            musicSource.Stop();

        lost = true;

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

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        //Will be replaced soon
        SceneManager.LoadScene("Main Menu");
    }


    void BeginPlay()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        UpdateScoreUI();
        musicSource = GameObject.Find("MusicPlayer").GetComponent<AudioSource>();
        levelLoader = GameObject.Find("Level Loader").GetComponent<LevelLoader>();
        UpdateLivesUI();
        gameOverPanel.SetActive(false);
        winningPanel.SetActive(false);
    }

    public void EnableInfiniteLives()
    {
        hasInfiniteLives = true;
        livesText.text = "Infinite Lives!";
    }

    void CheckSceneTransition()
    {
        if (level == Level.SoccerStadium)
        {
            if (score >= 20)
            {
                StartCoroutine(levelLoader.LoadLevel("BasketBall Stadium"));
            }
        }
        else if (level == Level.BasketBallStadium)
        {
            if (score >= 40)
            {
                StartCoroutine(levelLoader.LoadLevel("Tennis Playground"));
            }
        }
        else if (level == Level.TennisPlayground)
        {
            if (score >= 80)
            {
                Time.timeScale = 0;
                audioSource.PlayOneShot(winningSound);
                winningPanel.SetActive(true);
            }
        }
    }

}

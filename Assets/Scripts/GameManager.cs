using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    int score = 0;

    void Awake()
    {
        instance = this;
        Debug.Log("✅ GameManager فعال شد");
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
        Debug.Log("✅ امتیاز اضافه شد: " + score);
    }
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0; // توقف بازی
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

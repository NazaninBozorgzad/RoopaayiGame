using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;

    int score = 0;

    void Awake()
    {
        instance = this;
    }

    public void AddScore(int val)
    {
        score += val;
        scoreText.text = "Score: " + score;

        if (score == 20)
        {
            // توپ رو تغییر بده (توپ بسکتبال)
        }
        if (score == 40)
        {
            // توپ رو به توپ تنیس تغییر بده
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
}

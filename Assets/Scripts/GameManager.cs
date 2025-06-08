using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
}

using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isGood = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            if (isGood)
            {
                GameManager.instance.AddScore(5); // آیتم مثبت → +5
            }
            else
            {
                GameManager.instance.GameOver(); // آیتم منفی → باخت
            }

            Destroy(gameObject);
        }
    }
}

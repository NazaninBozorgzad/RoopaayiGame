using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force = 6f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
        Debug.Log("🟡 روی توپ کلیک شد");

        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

        if (GameManager.instance != null)
        {
            Debug.Log("✅ GameManager پیدا شد");
            GameManager.instance.AddScore(1);
        }
        else
        {
            Debug.Log("❌ GameManager هنوز null هست");
        }
    }
}
